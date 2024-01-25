using Hoax.WpfConverters.Base;
using System;
using System.Globalization;

namespace Hoax.WpfConverters
{
    public class ObjectToStringConverter : ConverterBase
    {
        /// <summary>
        /// Specifies a behavior is case when given value is null. Default is <see cref="NullHanding.ReturnEmptyString"/>.
        /// </summary>
        public NullHanding NullHanding { get; set; } = NullHanding.ReturnEmptyString;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result;

            if (value is null)
            {
                result = NullHanding switch
                {
                    NullHanding.ReturnNull          => null,
                    NullHanding.ReturnNullAsString  => "Null",
                    _                               => string.Empty
                };
            }
            else if (value is string stringValue)
                result = stringValue;
            else
                result = value.ToString();

            return ConvertNextIfNeeded(result);
        }
    }
}
