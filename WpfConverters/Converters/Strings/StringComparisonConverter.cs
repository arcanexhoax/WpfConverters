using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    /// <summary>
    /// String comparison operation.
    /// </summary>
    public enum StringComparisonOperation
    {
        IsNull,
        IsEmpty,
        IsNullOrEmpty,
        Equals,
        EqualsIgnoreCase,
        NotEquals,
        NotEqualsIgnoreCase
    }

    [ValueConversion(typeof(string), typeof(bool))]
    public class StringComparisonConverter : ConverterBase
    {
        /// <summary>
        /// Second operand for string equality operation.
        /// </summary>
        public string Operand { get; set; }

        /// <summary>
        /// String comparison operation.
        /// </summary>
        public StringComparisonOperation Operation { get; set; }

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
