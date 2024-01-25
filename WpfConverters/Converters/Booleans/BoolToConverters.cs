using Hoax.WpfConverters.Base;
using System.Windows;
using System.Windows.Media;

namespace Hoax.WpfConverters
{
    public class BoolToObjectConverter() : BoolToConverterBase<object>(null, null) { }
    public class BoolToDoubleConverter() : BoolToConverterBase<double>(0, 0) { }
    public class BoolToStringConverter() : BoolToConverterBase<string>(string.Empty, string.Empty) { }
    public class BoolToVisibilityConverter() : BoolToConverterBase<Visibility>(Visibility.Visible, Visibility.Collapsed) { }
    public class BoolToBrushConverter() : BoolToConverterBase<SolidColorBrush>(Brushes.Green, Brushes.Red) { }
    public class BoolToThicknessConverter() : BoolToConverterBase<Thickness>(new Thickness(0), new Thickness(0)) { }
    public class BoolToOpacityConverter() : BoolToConverterBase<double>(1, 0.5) { }
}
