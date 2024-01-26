using Hoax.WpfConverters.Base;
using System;
using System.Collections;
using System.Globalization;
using System.Windows;

namespace Hoax.WpfConverters
{
    public class CollectionToItemConverter : ConverterBase
    {
        /// <summary>
        /// Index of seeking collection's item.
        /// </summary>
        public int Index { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return DependencyProperty.UnsetValue;

            object item;

            if (value is IList collection)
                item = collection[Index];
            else if (value is IEnumerable enumerable)
            {
                int counter = 0;
                var e = enumerable.GetEnumerator();

                while (e.MoveNext() && counter < Index) 
                    counter++;

                item = e.Current;
            }
            else
            {
                throw new ArgumentException("The given value is not a collection.");
            }

            return ConvertNextIfNeeded(item);
        }
    }
}
