using Hoax.WpfConverters.Base;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Hoax.WpfConverters
{
    public class StringFormatConverter : ConverterBase
    {
        /// <summary>
        /// String format template for the given value(-s). 
        /// </summary>
        public string Format { get; set; } = string.Empty;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Format is null)
                return null;

            string result;

            if (value is IEnumerable enumerable and not string)
            {
                var castedEnumerable = enumerable.Cast<object>();
                result = string.Format(Format, castedEnumerable.ToArray());
            }
            else
            {
                result = string.Format(Format, value);
            }

            return ConvertNextIfNeeded(result);
        }
    }
}
