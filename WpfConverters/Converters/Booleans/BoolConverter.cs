using System.Globalization;
using System;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    /// <summary>
    /// Boolean operation
    /// </summary>
    public enum BoolOperation
    {
        /// <summary>
        /// Unary bool operation: !
        /// </summary>
        Not,
        /// <summary>
        /// Binary bool operation: &&
        /// </summary>
        And,
        /// <summary>
        /// Binary bool operation: ||
        /// </summary>
        Or,
        /// <summary>
        /// Binary bool operation: ^
        /// </summary>
        Xor,
        /// <summary>
        /// Binary bool operation: ==
        /// </summary>
        Equals,
        /// <summary>
        /// Binary bool operation: !=
        /// </summary>
        NotEquals
    }

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolConverter : ConverterBase
    {
        /// <summary>
        /// The second bool operand. Default is <see langword="false"/>.
        /// </summary>
        public bool Operand { get; set; } = false;

        /// <summary>
        /// Bool operation. Default is <see cref="BoolOperation.Not"/>.
        /// </summary>
        public BoolOperation Operation { get; set; } = BoolOperation.Not;

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
