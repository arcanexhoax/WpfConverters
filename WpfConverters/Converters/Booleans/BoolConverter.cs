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
        /// The second bool operand.
        /// </summary>
        public bool Operand { get; set; }

        /// <summary>
        /// Bool operation
        /// </summary>
        public BoolOperation Operation { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool firstOp = System.Convert.ToBoolean(value);

            bool result = Operation switch
            {
                BoolOperation.Not       => !firstOp,
                BoolOperation.And       => firstOp && Operand,
                BoolOperation.Or        => firstOp || Operand,
                BoolOperation.Xor       => firstOp ^ Operand,
                BoolOperation.NotEquals => firstOp != Operand,
                _                       => firstOp == Operand,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
