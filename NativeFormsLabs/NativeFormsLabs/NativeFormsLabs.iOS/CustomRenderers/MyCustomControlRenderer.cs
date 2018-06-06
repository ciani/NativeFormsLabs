[assembly: Xamarin.Forms.ExportRenderer(typeof(NativeFormsLabs.Core.Controls.MyCustomControl)
                                     , typeof(NativeFormsLabs.iOS.CustomRenderers.MyCustomControlRenderer))]
namespace NativeFormsLabs.iOS.CustomRenderers
{
    using CoreGraphics;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class MyCustomControlRenderer : ViewRenderer
    {
        private UIButton myButton;
        public MyCustomControlRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                myButton = new UIButton(UIButtonType.InfoLight);
                SetNativeControl(myButton);
            }
        }
    }
}