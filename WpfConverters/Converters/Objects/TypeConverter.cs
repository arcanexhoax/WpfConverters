using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class TypeConverter : ConverterBase
    {
        /// <summary>
        /// Target type that value converts in. Default is <see cref="string"/>.
        /// </summary>
        public Type To { get; set; } = typeof(string);

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = DependencyProperty.UnsetValue;

            if (value is IConvertible || value == null)
            {
                result = System.Convert.ChangeType(value, To, culture);
            }
            else
            {
                var typeConverter = TypeDescriptor.GetConverter(value);

                if (typeConverter.CanConvertTo(To))
                    result = typeConverter.ConvertTo(null, culture, value, To);
            }

            return ConvertNextIfNeeded(result);
        }
    }
}
