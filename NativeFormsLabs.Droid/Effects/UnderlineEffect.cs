[assembly: Xamarin.Forms.ResolutionGroupName("DevsDNA")]
[assembly: Xamarin.Forms.ExportEffect(typeof(NativeFormsLabs.Droid.Effects.UnderlineEffect), "UnderlineEffect")]
namespace NativeFormsLabs.Droid.Effects
{
    using Android.Graphics;
    using Android.Widget;
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    public class UnderlineEffect : PlatformEffect
    {
        /// <summary>
        /// Method that is called after the effect is attached and made valid.
        /// </summary>
        protected override void OnAttached()
        {
            SetUnderline(true);
        }

        /// <summary>
        /// Method that is called after the effect is detached and invalidated.
        /// </summary>
        protected override void OnDetached()
        {
            SetUnderline(false);
        }

        /// <summary>
        /// Method that is called when a element property has changed.
        /// </summary>
        /// <param name="args">The arguments for the property changed event.</param>
        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
                SetUnderline(true);
        }

        private void SetUnderline(bool underlined)
        {
            try
            {
                var textView = (TextView)Control;
                if (underlined)
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                else
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
            }
        }
    }
}