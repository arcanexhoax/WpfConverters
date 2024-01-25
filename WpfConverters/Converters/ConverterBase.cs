using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Hoax.WpfConverters.Base
{
    public abstract class ConverterBase : MarkupExtension, IValueConverter
    {
        /// <summary>
        /// Next converter that will use the result of the current converting as a "binding value".
        /// </summary>
        public IValueConverter Then { get; set; }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => this;

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

        public object ConvertNextIfNeeded(object result)
        {
            return Then?.Convert(result, result.GetType(), null, CultureInfo.CurrentCulture) ?? result;
        }
    }
}
