using Hoax.WpfConverters.Base;
using Hoax.WpfConverters.Utils;
using System;
using System.Globalization;

namespace Hoax.WpfConverters
{
    public class ObjectComparisonConverter : ConverterBase
    {
        /// <summary>
        /// Second operand to compare with given value.
        /// </summary>
        public object Operand { get; set; }

        /// <summary>
        /// An operation for objects comparison.
        /// </summary>
        public ObjectComparisonOperation Operation { get; set; } = ObjectComparisonOperation.Equals;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = Operation switch
            {
                ObjectComparisonOperation.IsNull    => value is null,
                ObjectComparisonOperation.IsNotNull => value is not null,
                ObjectComparisonOperation.NotEquals => CompareTwoOperands(value, Operand, false),
                _                                   => CompareTwoOperands(value, Operand, true),
            };

            return ConvertNextIfNeeded(result);
        }

        private bool CompareTwoOperands(object value1, object value2, bool useEquals)
        {
            if (value1 == null && value2 == null)
                return useEquals;

            if (value1.IsNumber() && value2.IsNumber())
            {
                double num1 = System.Convert.ToDouble(value1);
                double num2 = System.Convert.ToDouble(value2);

                return num1 == num2 == useEquals;
            }

            bool equals = value1?.Equals(value2) ?? false;
            return useEquals == equals;
        }
    }
}
