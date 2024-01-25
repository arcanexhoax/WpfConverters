using Hoax.WpfConverters.Base;
using System;
using System.Globalization;

namespace Hoax.WpfConverters
{
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
