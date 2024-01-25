using Hoax.WpfConverters.Base;
using System;
using System.Collections;
using System.Globalization;
using System.Windows;

namespace Hoax.WpfConverters
{
    public class CollectionToCountConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return DependencyProperty.UnsetValue;

            int count = 0;

            if (value is ICollection collection)
                count = collection.Count;
            else if (value is object[] array)
                count = array.Length;
            else if (value is IEnumerable enumerable)
            {
                var e = enumerable.GetEnumerator();

                while (e.MoveNext())
                    count++;
            }
            else
            {
                throw new ArgumentException("The given value is not a collection.");
            }

            return ConvertNextIfNeeded(count);
        }
    }
}
