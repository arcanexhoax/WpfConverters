using System;
using System.Globalization;
using System.Windows;
using WpfConverters.Converters;

namespace WpfConverters
{
    public class VisibilityToBoolConverter : ConverterBase
    {
        /// <summary>
        /// Bool value for <see cref="Visibility.Visible"/> state. Default if <see cref="true"/>.
        /// </summary>
        public bool ForVisible { get; set; } = true;

        /// <summary>
        /// Bool value for <see cref="Visibility.Hidden"/> state. Default is <see cref="false"/>.
        /// </summary>
        public bool ForHidden { get; set; } = false;

        /// <summary>
        /// Bool value for <see cref="Visibility.Collapsed"/> state. Default is <see cref="false"/>.
        /// </summary>
        public bool ForCollapsed { get; set; } = false;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not Visibility visibility)
                throw new ArgumentException($"Given value has no {nameof(Visibility)} type.");

            bool result = visibility switch
            {
                Visibility.Visible => ForVisible,
                Visibility.Hidden => ForHidden,
                _ => ForCollapsed,
            };

            return ConvertNextIfNeeded(result);
        }
    }
}
