using System.Globalization;
using System;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Boolean operation
    /// </summary>
    public enum BoolOperation
    {
        Not,
        And,
        Or,
        Xor,
        Equals,
        NotEquals
    }

    public class BoolConverter : ConverterBase
    {
        /// <summary>
        /// The second bool operand. This operand will be the first if <see cref="ExtraOperand"/> is specified. 
        /// </summary>
        public bool? Operand { get; set; }

        /// <summary>
        /// The second bool operand for binding value or <see cref="Operand"/> if it is specified.
        /// </summary>
        public bool? ExtraOperand { get; set; }

        /// <summary>
        /// Bool operation
        /// </summary>
        public BoolOperation Operation { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bindingValue = System.Convert.ToBoolean(value);

            if (Operation == BoolOperation.Not)
            {
                bool operand;

                if (Operand.HasValue)
                    operand = Operand.Value;
                else if (ExtraOperand.HasValue) 
                    operand = ExtraOperand.Value;
                else
                    operand = bindingValue;

                return ConvertNextIfNeeded(!operand);
            }

            (bool firstOp, bool secondOp) = (Operand, ExtraOperand) switch
            {
                (null, null)         => throw new ArgumentException($"Second operand ({nameof(Operand)} or {nameof(ExtraOperand)}) was not specified."),
                (null, not null)     => (bindingValue, ExtraOperand.Value),
                (not null, null)     => (bindingValue, Operand.Value),
                (not null, not null) => (Operand.Value, ExtraOperand.Value)
            };

            bool result = Operation switch
            {
                BoolOperation.And       => firstOp && secondOp,
                BoolOperation.Or        => firstOp || secondOp,
                BoolOperation.Xor       => firstOp ^ secondOp,
                BoolOperation.NotEquals => firstOp != secondOp,
                _                       => firstOp == secondOp,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
