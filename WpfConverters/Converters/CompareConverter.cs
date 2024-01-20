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
        /// The second comparison operand. This operand will be the first if <see cref="ExtraOperand"/> is specified. 
        /// </summary>
        public double? Operand { get; set; }

        /// <summary>
        /// The second comparison operand for binding value or <see cref="Operand"/> if it is specified.
        /// </summary>
        public double? ExtraOperand { get; set; }

        /// <summary>
        /// Comparison operation between 2 values
        /// </summary>
        public ComparisonOperation Operation { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double bindingValue = System.Convert.ToDouble(value);

            (double firstOp, double secondOp) = (Operand, ExtraOperand) switch
            {
                (null, null)         => throw new ArgumentException($"Second operand ({nameof(Operand)} or {nameof(ExtraOperand)}) was not specified."),
                (null, not null)     => (bindingValue, ExtraOperand.Value),
                (not null, null)     => (bindingValue, Operand.Value),
                (not null, not null) => (Operand.Value, ExtraOperand.Value)
            };

            bool result = Operation switch
            {
                ComparisonOperation.Less         => firstOp < secondOp,
                ComparisonOperation.LessOrEquals => firstOp <= secondOp,
                ComparisonOperation.More         => firstOp > secondOp,
                ComparisonOperation.MoreOrEquals => firstOp >= secondOp,
                ComparisonOperation.NotEquals    => firstOp != secondOp,
                _                                => firstOp == secondOp,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
