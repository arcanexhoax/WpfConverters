using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class StringFormatConverter : ConverterBase
    {
        /// <summary>
        /// The argument to format.
        /// </summary>
        public string Argument { get; set; }

        /// <summary>
        /// The arguments to format. They will be used only if <see cref="Argument"/> is not set.
        /// </summary>
        public Collection<string> Arguments { get; } = [];

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = targetType == typeof(string) ? (string)value : value.ToString();
            string result;

            if (Argument is not null)
                result = string.Format(input, Argument);
            else if (Arguments is not null && Arguments.Count > 0)
                result = string.Format(input, args: Arguments.ToArray());
            else
                throw new ArgumentException($"No of the parameters ({nameof(Argument)} or {nameof(Arguments)}) were set.");

            return ConvertNextIfNeeded(result);
        }
    }
}
