using Hoax.WpfConverters.Base;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hoax.WpfConverters
{
    public class ColorToBrushConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                SolidColorBrush result = new(color);
                return ConvertNextIfNeeded(result);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
