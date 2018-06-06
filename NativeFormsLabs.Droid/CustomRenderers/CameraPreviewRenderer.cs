[assembly:Xamarin.Forms.ExportRenderer(typeof(NativeFormsLabs.Core.Controls.CameraPreview)
                                      ,typeof(NativeFormsLabs.Droid.CustomRenderers.CameraPreviewRenderer))]
namespace NativeFormsLabs.Droid.CustomRenderers
{
    using Android.Content;
    using Android.Hardware;
    using NativeFormsLabs.Core.Controls;
    using Xamarin.Forms.Platform.Android;
    public class CameraPreviewRenderer : ViewRenderer<CameraPreview, Droid.Controls.CameraPreview>
    {
        private Droid.Controls.CameraPreview cameraPreview;
        protected CameraPreviewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                cameraPreview = new Controls.CameraPreview(Context);
                SetNativeControl(cameraPreview);
            }
            if (e.OldElement != null)
            {
                cameraPreview.Click -= CameraPreview_Click;
            }
            if (e.NewElement != null)
            {
                Control.Preview = Camera.Open((int)e.NewElement.Camera);
                cameraPreview.Click += CameraPreview_Click;
            } 
        }

        private void CameraPreview_Click(object sender, System.EventArgs e)
        {
            if (cameraPreview.IsPreviewing)
            {
                cameraPreview.Preview.StopPreview();
                cameraPreview.IsPreviewing = false;
            }
            else
            {
                cameraPreview.Preview.StartPreview();
                cameraPreview.IsPreviewing = true;
            }
        }
    }
}