using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfConverters.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : ConverterBase
    {
        /// <summary>
        /// The <see cref="Visibility"/> behavior when binding value is <see langword="true"/>. Default value is <see cref="Visibility.Visible"/>.
        /// </summary>
        public Visibility ForTrue { get; set; } = Visibility.Visible;

        /// <summary>
        /// The <see cref="Visibility"/> behavior when binding value is <see langword="false"/>. Default value is <see cref="Visibility.Collapsed"/>.
        /// </summary>
        public Visibility ForFalse { get; set; } = Visibility.Collapsed;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool input = System.Convert.ToBoolean(value);
            Visibility result = input ? ForTrue : ForFalse;

            return ConvertNextIfNeeded(result);
        }
    }
}
