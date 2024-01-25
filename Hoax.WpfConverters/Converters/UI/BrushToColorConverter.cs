using Hoax.WpfConverters.Base;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hoax.WpfConverters
{
    public class BrushToColorConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush brush)
            {
                Color result = brush.Color;
                return ConvertNextIfNeeded(result);
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
