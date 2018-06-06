[assembly:Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Button)
                                     , typeof(NativeFormsLabs.iOS.CustomRenderers.CustomButtonRenderer))]
namespace NativeFormsLabs.iOS.CustomRenderers
{
    using System.ComponentModel;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetTitleColor(UIColor.Brown, UIControlState.Disabled);
                Control.SetTitleColor(UIColor.Red, UIControlState.Normal);
                Control.Layer.BorderColor = UIColor.DarkGray.CGColor;
                Control.Layer.BorderWidth = 3;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}