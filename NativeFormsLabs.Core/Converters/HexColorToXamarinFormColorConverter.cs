namespace NativeFormsLabs.Core.Converters
{
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class HexColorToXamarinFormColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueHexColor = value.ToString();
            return Xamarin.Forms.Color.FromHex(valueHexColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
