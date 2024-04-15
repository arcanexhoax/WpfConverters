using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.UI
{
    internal class BrushColorTest
    {
        [Test]
        public void SimpleBrushToColorTest()
        {
            Color opColor = Colors.Blue;
            object op1 = new SolidColorBrush(opColor);
            object op2 = new();
            object unset = DependencyProperty.UnsetValue;

            var con = new BrushToColorConverter();

            Color actualResult = (Color)con.Convert(op1, typeof(SolidColorBrush), null, CultureInfo.InvariantCulture);
            object actualResult2 = con.Convert(op2, typeof(object), null, CultureInfo.InvariantCulture);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult, Is.EqualTo(opColor));
                Assert.That(actualResult2, Is.EqualTo(unset));
            });
        }

        [Test]
        public void SimpleColorToBrushTest()
        {
            SolidColorBrush scb = new(Colors.Aqua);
            object op1 = Colors.Aqua;
            object op2 = new();
            object unset = DependencyProperty.UnsetValue;

            var con = new ColorToBrushConverter();

            SolidColorBrush actualResult = (SolidColorBrush)con.Convert(op1, typeof(Color), null, CultureInfo.InvariantCulture);
            object actualResult2 = con.Convert(op2, typeof(object), null, CultureInfo.InvariantCulture);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Color, Is.EqualTo(scb.Color));
                Assert.That(actualResult2, Is.EqualTo(unset));
            });
        }
    }
}
