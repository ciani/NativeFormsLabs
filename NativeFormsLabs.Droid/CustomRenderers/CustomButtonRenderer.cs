[assembly: Xamarin.Forms.ExportRenderer(typeof(NativeFormsLabs.Core.Controls.MyCustomControl)
                                     , typeof(NativeFormsLabs.Droid.CustomRenderers.CustomButtonRenderer))]
namespace NativeFormsLabs.Droid.CustomRenderers
{
    using Android.Content;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class CustomButtonRenderer : ViewRenderer
    {
        private Android.Widget.Button myButton;

        public static void Init()
        {            
        }

        protected CustomButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                myButton = new Android.Widget.Button(Android.App.Application.Context);
                SetNativeControl(myButton);
            }
            if (e.OldElement != null)
            {
                // Unsubscribe
            }
            if (e.NewElement != null)
            {
                // Subscribe
            }
        }
    }
}