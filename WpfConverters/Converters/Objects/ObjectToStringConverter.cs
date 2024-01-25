﻿using Hoax.WpfConverters.Base;
using System;
using System.Globalization;

namespace Hoax.WpfConverters
{
    public class ObjectToStringConverter : ConverterBase
    {
        /// <summary>
        /// Specifies a behavior is case when given value is null. Default is <see cref="NullHanding.ReturnEmptyString"/>.
        /// </summary>
        public NullHanding NullHanding { get; set; } = NullHanding.ReturnEmptyString;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = NullHanding switch
            {
                NullHanding.ReturnNull when value is null         => null,
                NullHanding.ReturnNullAsString when value is null => "Null",
                NullHanding.ReturnEmptyString when value is null  => string.Empty,
                _                                                 => value.ToString()
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
