﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConverters.Converters
{
    /// <summary>
    /// An operation for objects comparison.
    /// </summary>
    public enum ObjectComparisonOperation
    {
        Equals,
        NotEquals,
        IsNull,
        IsNotNull,
    }

    public class ObjectToBoolConverter : ConverterBase
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
                ObjectComparisonOperation.NotEquals => value == Operand,
                _                                   => value != Operand,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
