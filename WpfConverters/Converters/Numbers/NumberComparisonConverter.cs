using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Comparison operation between 2 values.
    /// </summary>
    public enum ComparisonOperation
    {
        /// <summary>
        /// Comparison operation: <
        /// </summary>
        Less,
        /// <summary>
        /// Comparison operation: <=
        /// </summary>
        LessOrEquals,
        /// <summary>
        /// Comparison operation: >
        /// </summary>
        More,
        /// <summary>
        /// Comparison operation: >=
        /// </summary>
        MoreOrEquals,
        /// <summary>
        /// Comparison operation: ==
        /// </summary>
        Equals,
        /// <summary>
        /// Comparison operation: ==
        /// </summary>
        NotEquals,
    }

    [ValueConversion(typeof(double), typeof(bool))]
    public class NumberComparisonConverter : ConverterBase
    {
        /// <summary>
        /// The second comparison operand. Default is 0.
        /// </summary>
        public double Operand { get; set; } = 0;

        /// <summary>
        /// Comparison operation between 2 values. Default is <see cref="ComparisonOperation.Equals"/>.
        /// </summary>
        public ComparisonOperation Operation { get; set; } = ComparisonOperation.Equals;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double firstOp = System.Convert.ToDouble(value);

            bool result = Operation switch
            {
                ComparisonOperation.Less => firstOp < Operand,
                ComparisonOperation.LessOrEquals => firstOp <= Operand,
                ComparisonOperation.More => firstOp > Operand,
                ComparisonOperation.MoreOrEquals => firstOp >= Operand,
                ComparisonOperation.NotEquals => firstOp != Operand,
                _ => firstOp == Operand,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
