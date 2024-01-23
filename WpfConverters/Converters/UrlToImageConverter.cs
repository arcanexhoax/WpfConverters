using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfConverters.Converters
{
    public class UrlToImageConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new BitmapImage();
            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;

            Uri sourceUri = value switch
            {
                string stringUri => new Uri(stringUri, UriKind.RelativeOrAbsolute),
                Uri uri          => uri,
                _                => null
            };

            if (sourceUri is null)
                return DependencyProperty.UnsetValue;

            result.UriSource = sourceUri;
            result.EndInit();
            return ConvertNextIfNeeded(result);
        }
    }
}
