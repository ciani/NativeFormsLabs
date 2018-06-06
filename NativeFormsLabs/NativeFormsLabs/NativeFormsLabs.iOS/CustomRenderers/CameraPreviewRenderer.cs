[assembly: Xamarin.Forms.ExportRenderer(typeof(NativeFormsLabs.Core.Controls.CameraPreview)
                                     , typeof(NativeFormsLabs.iOS.CustomRenderers.CameraPreviewRenderer))]
namespace NativeFormsLabs.iOS.CustomRenderers
{
    using System.ComponentModel;
    using NativeFormsLabs.Core.Controls;
    using NativeFormsLabs.iOS.Controls;
    using Xamarin.Forms.Platform.iOS;
    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
    {
        private UICameraPreview uiCameraPreview;
        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                uiCameraPreview = new UICameraPreview(Element.Camera);
                SetNativeControl(uiCameraPreview);
            }
            if (e.OldElement != null)
            {
                uiCameraPreview.Tapped -= Camera_Tapped;
            }
            if (e.NewElement != null)
            {
                uiCameraPreview.Tapped += Camera_Tapped;
            }
        }
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        private void Camera_Tapped(object sender, System.EventArgs e)
        {
            if (uiCameraPreview.IsPreviewing)
            {
                uiCameraPreview.CaptureSession.StopRunning();
                uiCameraPreview.IsPreviewing = false;
            }
            else
            {
                uiCameraPreview.CaptureSession.StartRunning();
                uiCameraPreview.IsPreviewing = true;
            }
        }

    }
}