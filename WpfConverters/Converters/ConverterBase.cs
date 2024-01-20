using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfConverters.Converters
{
    public abstract class ConverterBase : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Next converter that will use the result of the current converting as a "binding value".
        /// </summary>
        public ConverterBase Then { get; set; }

        public object ConvertFrom(object value)
        {
            return Convert(value, value.GetType(), null, CultureInfo.CurrentCulture);
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
