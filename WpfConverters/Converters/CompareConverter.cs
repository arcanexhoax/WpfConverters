using System;
using System.Globalization;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Comparison operation between 2 values
    /// </summary>
    public enum ComparisonOperation
    {
        Less,
        LessOrEquals,
        More,
        MoreOrEquals,
        Equals,
        NotEquals,
    }

    public class CompareConverter : ConverterBase
    {
        /// <summary>
        /// The second comparison operand.
        /// </summary>
        public double Operand { get; set; }

        /// <summary>
        /// Comparison operation between 2 values
        /// </summary>
        public ComparisonOperation Operation { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double firstOp = System.Convert.ToDouble(value);

            bool result = Operation switch
            {
                ComparisonOperation.Less         => firstOp < Operand,
                ComparisonOperation.LessOrEquals => firstOp <= Operand,
                ComparisonOperation.More         => firstOp > Operand,
                ComparisonOperation.MoreOrEquals => firstOp >= Operand,
                ComparisonOperation.NotEquals    => firstOp != Operand,
                _                                => firstOp == Operand,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
