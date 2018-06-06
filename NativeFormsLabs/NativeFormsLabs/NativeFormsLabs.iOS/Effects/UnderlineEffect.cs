[assembly: Xamarin.Forms.ExportEffect(typeof(NativeFormsLabs.iOS.Effects.UnderlineEffect), "UnderlineEffect")]
namespace NativeFormsLabs.iOS.Effects
{
    using Foundation;
    using System;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

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
            {
                SetUnderline(true);
            }
        }

        private void SetUnderline(bool underlined)
        {
            try
            {
                var label = (UILabel)Control;
                var text = (NSMutableAttributedString)label.AttributedText;
                var range = new NSRange(0, text.Length);

                if (underlined)
                    text.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
                else
                    text.RemoveAttribute(UIStringAttributeKey.UnderlineStyle, range);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
            }
        }
    }
}