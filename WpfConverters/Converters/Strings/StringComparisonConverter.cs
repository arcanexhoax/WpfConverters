using System;
using System.Globalization;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class StringComparisonConverter : ConverterBase
    {
        /// <summary>
        /// Second operand for string equality operation.
        /// </summary>
        public string Operand { get; set; }

        /// <summary>
        /// String comparison operation. Default is <see cref="StringComparisonOperation.Equals"/>.
        /// </summary>
        public StringComparisonOperation Operation { get; set; } = StringComparisonOperation.Equals;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = targetType == typeof(string) ? (string)value : value?.ToString();

            bool result = Operation switch
            {
                StringComparisonOperation.IsNull              => input is null,
                StringComparisonOperation.IsEmpty             => input == string.Empty,
                StringComparisonOperation.IsNullOrEmpty       => string.IsNullOrEmpty(input),
                StringComparisonOperation.NotEquals           => !string.Equals(input, Operand),
                StringComparisonOperation.NotEqualsIgnoreCase => !string.Equals(input, Operand, StringComparison.CurrentCultureIgnoreCase),
                StringComparisonOperation.EqualsIgnoreCase    => string.Equals(input, Operand, StringComparison.CurrentCultureIgnoreCase),
                _                                             => string.Equals(input, Operand),
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
