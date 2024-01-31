using Hoax.WpfConverters.Base;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace Hoax.WpfConverters
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

            if (value is null)
            {
                if (To.IsValueType)
                    return Activator.CreateInstance(To);

                return null;
            }

            if (value is char charValue)
            {
                if (To == typeof(double))
                    return (double)charValue;
                else if (To == typeof(float)) 
                    return (float)charValue;
                else if (To == typeof(decimal))
                    return (decimal)charValue;
            }

            if (value is IConvertible || value == null)
            {
                try
                {
                    result = System.Convert.ChangeType(value, To, culture);
                }
                catch (FormatException)
                {
                    if (value is string stringValue)
                        return stringValue is not null and not [];

                    throw;
                }
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
