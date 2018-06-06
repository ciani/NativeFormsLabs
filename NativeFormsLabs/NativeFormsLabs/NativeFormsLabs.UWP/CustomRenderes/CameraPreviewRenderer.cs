[assembly: Xamarin.Forms.Platform.UWP.ExportRenderer(typeof(NativeFormsLabs.Core.Controls.CameraPreview)
    , typeof(NativeFormsLabs.UWP.CustomRenderes.CameraPreviewRenderer))]
namespace NativeFormsLabs.UWP.CustomRenderes
{
    using NativeFormsLabs.Core.Controls;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.Devices.Enumeration;
    using Windows.Graphics.Display;
    using Windows.Media.Capture;
    using Windows.System.Display;
    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Xamarin.Forms.Platform.UWP;

    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, Windows.UI.Xaml.Controls.CaptureElement>
    {
        readonly DisplayInformation displayInformation = DisplayInformation.GetForCurrentView();
        DisplayOrientations displayOrientation = DisplayOrientations.Portrait;
        readonly DisplayRequest displayRequest = new DisplayRequest();

        // Rotation metadata to apply to preview stream (https://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868174.aspx)
        static readonly Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1"); // (MF_MT_VIDEO_ROTATION)

        static readonly SemaphoreSlim mediaCaptureLifeLock = new SemaphoreSlim(1);

        MediaCapture mediaCapture;
        CaptureElement captureElement;
        bool isInitialized;
        bool isPreviewing;
        bool externalCamera;
        bool mirroringPreview;

        Application app;

        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                app = Application.Current;
                app.Suspending += OnAppSuspending;
                app.Resuming += OnAppResuming;

                captureElement = new CaptureElement();
                captureElement.Stretch = Stretch.UniformToFill;

                SetupCamera();
                SetNativeControl(captureElement);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
                Tapped -= OnCameraPreviewTapped;
                displayInformation.OrientationChanged -= OnOrientationChanged;
                app.Suspending -= OnAppSuspending;
                app.Resuming -= OnAppResuming;
            }
            if (e.NewElement != null)
            {
                // Subscribe
                Tapped += OnCameraPreviewTapped;
            }
        }

        async void SetupCamera()
        {
            displayOrientation = displayInformation.CurrentOrientation;
            displayInformation.OrientationChanged += OnOrientationChanged;
            await InitializeCameraAsync();
        }

        #region Event Handlers

        async void OnOrientationChanged(DisplayInformation sender, object args)
        {
            displayOrientation = sender.CurrentOrientation;
            if (isPreviewing)
            {
                await SetPreviewRotationAsync();
            }
        }

        async void OnCameraPreviewTapped(object sender, TappedRoutedEventArgs e)
        {
            if (isPreviewing)
            {
                await StopPreviewAsync();
            }
            else
            {
                await StartPreviewAsync();
            }
        }

        #endregion

        #region Camera

        async Task InitializeCameraAsync()
        {
            await mediaCaptureLifeLock.WaitAsync();

            if (mediaCapture == null)
            {
                // Attempt to get the back camera, but use any camera if not
                var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Back);
                if (cameraDevice == null)
                {
                    Debug.WriteLine("No camera found");
                    return;
                }

                mediaCapture = new MediaCapture();
                var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };
                try
                {
                    await mediaCapture.InitializeAsync(settings);
                    isInitialized = true;
                }

                catch (UnauthorizedAccessException)
                {
                    Debug.WriteLine("Camera access denied");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception initializing MediaCapture - {0}: {1}", cameraDevice.Id, ex.ToString());
                }
                finally
                {
                    mediaCaptureLifeLock.Release();
                }

                if (isInitialized)
                {
                    if (cameraDevice.EnclosureLocation == null || cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Unknown)
                    {
                        externalCamera = true;
                    }
                    else
                    {
                        // Camera is on device
                        externalCamera = false;

                        // Mirror preview if camera is on front panel
                        mirroringPreview = (cameraDevice.EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front);
                    }
                    await StartPreviewAsync();
                }
            }
            else
            {
                mediaCaptureLifeLock.Release();
            }
        }

        async Task StartPreviewAsync()
        {
            // Prevent the device from sleeping while the preview is running
            displayRequest.RequestActive();

            // Setup preview source in UI and mirror if required
            captureElement.Source = mediaCapture;
            captureElement.FlowDirection = mirroringPreview ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

            // Start preview
            await mediaCapture.StartPreviewAsync();
            isPreviewing = true;

            if (isPreviewing)
            {
                await SetPreviewRotationAsync();
            }
        }

        async Task StopPreviewAsync()
        {
            isPreviewing = false;
            await mediaCapture.StopPreviewAsync();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Allow device screen to sleep now preview is stopped
                displayRequest.RequestRelease();
            });
        }

        async Task SetPreviewRotationAsync()
        {
            // Only update the orientation if the camera is mounted on the device
            if (externalCamera)
            {
                return;
            }

            // Derive the preview rotation
            int rotation = ConvertDisplayOrientationToDegrees(displayOrientation);

            // Invert if mirroring
            if (mirroringPreview)
            {
                rotation = (360 - rotation) % 360;
            }

            // Add rotation metadata to preview stream
            var props = mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview);
            props.Properties.Add(RotationKey, rotation);
            await mediaCapture.SetEncodingPropertiesAsync(MediaStreamType.VideoPreview, props, null);
        }

        async Task CleanupCameraAsync()
        {
            await mediaCaptureLifeLock.WaitAsync();

            if (isInitialized)
            {
                if (isPreviewing)
                {
                    await StopPreviewAsync();
                }
                isInitialized = false;
            }
            if (captureElement != null)
            {
                captureElement.Source = null;
            }
            if (mediaCapture != null)
            {
                mediaCapture.Dispose();
                mediaCapture = null;
            }
        }

        #endregion

        #region Helpers

        async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {

            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            var desiredDevice = allVideoDevices.FirstOrDefault(d => d.EnclosureLocation != null && d.EnclosureLocation.Panel == desiredPanel);
            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }

        int ConvertDisplayOrientationToDegrees(DisplayOrientations orientation)
        {
            switch (orientation)
            {
                case DisplayOrientations.Portrait:
                    return 90;
                case DisplayOrientations.LandscapeFlipped:
                    return 180;
                case DisplayOrientations.PortraitFlipped:
                    return 270;
                case DisplayOrientations.Landscape:
                default:
                    return 0;
            }
        }

        #endregion

        #region Lifecycle

        async void OnAppSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await CleanupCameraAsync();
            displayInformation.OrientationChanged -= OnOrientationChanged;
            deferral.Complete();
        }

        void OnAppResuming(object sender, object o)
        {
            displayOrientation = displayInformation.CurrentOrientation;
            displayInformation.OrientationChanged += OnOrientationChanged;
        }

        #endregion
    }
}
