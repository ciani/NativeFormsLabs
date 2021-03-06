﻿[assembly: Xamarin.Forms.ResolutionGroupName("DevsDNA")]
[assembly: Xamarin.Forms.ExportEffect(typeof(NativeFormsLabs.UWP.Effects.FocusEffect), "Focuseffect")]
namespace NativeFormsLabs.UWP.Effects
{
    using System;
    using System.Diagnostics;
    using Windows.UI;
    using Windows.UI.Xaml.Media;
    using Xamarin.Forms.Platform.UWP;

    public class FocusEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                (Control as Windows.UI.Xaml.Controls.Control).Background = new SolidColorBrush(Colors.Cyan);
                (Control as FormsTextBox).BackgroundFocusBrush = new SolidColorBrush(Colors.White);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }        
    }
}
