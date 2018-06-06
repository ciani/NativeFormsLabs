[assembly: Xamarin.Forms.ResolutionGroupName("DevsDNA")]
[assembly: Xamarin.Forms.ExportEffect(typeof(NativeFormsLabs.iOS.Effects.FocusEffect), "Focuseffect")]
namespace NativeFormsLabs.iOS.Effects
{
    using System;
    using System.ComponentModel;
    using UIKit;
    using Xamarin.Forms.Platform.iOS;

    public class FocusEffect : PlatformEffect
    {
        UIColor backgroundColor;
        protected override void OnAttached()
        {
            try
            {
                Control.BackgroundColor = backgroundColor = UIColor.FromRGB(204, 153, 255);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            try
            {
                if (args.PropertyName == "IsFocused")
                {
                    if (Control.BackgroundColor == backgroundColor)
                    {
                        Control.BackgroundColor = UIColor.White;
                    }
                    else
                    {
                        Control.BackgroundColor = backgroundColor;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}