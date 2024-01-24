using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class CollectionToStringConverter : ConverterBase
    {
        /// <summary>
        /// A separator between given collection items. Default is ", ".
        /// </summary>
        public string Separator { get; set; } = ", ";
        
        /// <summary>
        /// A string converter of the given collection items. Default is <see cref="ToStringConverter"/>.
        /// </summary>
        public IValueConverter StringConverter { get; set; } = new ToStringConverter();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not IEnumerable collection || value is null)
                return DependencyProperty.UnsetValue;

            List<object> list = [];

            foreach (var item in collection)
            {
                list.Add(StringConverter.Convert(item, typeof(object), null, culture));
            }

            string result = string.Join(Separator, list);
            return ConvertNextIfNeeded(result);
        }
    }
}
