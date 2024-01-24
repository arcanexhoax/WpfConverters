using System;
using System.Globalization;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class NumberComparisonConverter : ConverterBase
    {
        /// <summary>
        /// The second comparison operand. Default is 0.
        /// </summary>
        public double Operand { get; set; } = 0;

        /// <summary>
        /// Comparison operation between 2 values. Default is <see cref="NumberComparisonOperation.Equals"/>.
        /// </summary>
        public NumberComparisonOperation Operation { get; set; } = NumberComparisonOperation.Equals;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double firstOp = System.Convert.ToDouble(value);

            bool result = Operation switch
            {
                NumberComparisonOperation.Less => firstOp < Operand,
                NumberComparisonOperation.LessOrEquals => firstOp <= Operand,
                NumberComparisonOperation.More => firstOp > Operand,
                NumberComparisonOperation.MoreOrEquals => firstOp >= Operand,
                NumberComparisonOperation.NotEquals => firstOp != Operand,
                _ => firstOp == Operand,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
