using Hoax.WpfConverters.Base;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.Booleans
{
    internal class BoolToConverterTest
    {
        [Test]
        public void BoolToObjectTest() => BoolTo(new object(), new object(), new BoolToObjectConverter());

        [Test]
        public void BoolToDoubleTest() => BoolTo(1, 2, new BoolToDoubleConverter());

        [Test]
        public void BoolToStringTest() => BoolTo("1", "2", new BoolToStringConverter());

        [Test]
        public void BoolToVisibilityTest() => BoolTo(Visibility.Visible, Visibility.Collapsed, new BoolToVisibilityConverter());

        [Test]
        public void BoolToBrushTest() => BoolTo(Brushes.AliceBlue, Brushes.Azure, new BoolToBrushConverter());

        [Test]
        public void BoolToThicknessTest() => BoolTo(new Thickness(1), new Thickness(2), new BoolToThicknessConverter());

        [Test]
        public void BoolToOpacityTest() => BoolTo(1, 2, new BoolToOpacityConverter());

        public void BoolTo<T>(T forTrue, T forFalse, BoolToConverterBase<T> con)
        {
            bool op1 = false;
            bool op2 = true;

            con.ForFalse = forFalse;
            con.ForTrue = forTrue;

            object falseActualResult = (T)con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture);
            object trueActualResult = (T)con.Convert(op2, typeof(bool), null, CultureInfo.CurrentCulture);

            Assert.Multiple(() =>
            {
                Assert.That(falseActualResult, Is.EqualTo(forFalse));
                Assert.That(trueActualResult, Is.EqualTo(forTrue));
            });
        }

        [Test]
        public void BoolToStringWithManyConvertersTest()
        {
            bool op1 = false;
            string op2 = "string";
            bool op3 = false;

            var con = new BoolToStringConverter()
            {
                ForFalse = op2,
                Then = new StringCaseConverter()
                {
                    Operation = CaseOperation.ToUpper,
                    Then = new StringComparisonConverter()
                    {
                        Operation = StringComparisonOperation.IsNull
                    }
                }
            };

            bool actualResult = (bool)con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture);

            Assert.That(actualResult, Is.EqualTo(op3));
        }

        [Test]
        public void BoolToBrushWithInvalidInputTest()
        {
            string op1 = "string";

            var con = new BoolToBrushConverter();

            Assert.Catch<FormatException>(() => con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture));
        }
    }
}
