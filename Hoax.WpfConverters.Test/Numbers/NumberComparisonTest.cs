using System.Globalization;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.Numbers
{
    internal class NumberComparisonTest
    {
        [Test]
        public void SimpleComparisonOperationsTest()
        {
            List<(double FirstOp, double SecondOp, NumberComparisonOperation Operation, bool Result)> ops = [
                (1, 2, NumberComparisonOperation.Less, true),
                (2, 2, NumberComparisonOperation.Less, false),
                (2, 1, NumberComparisonOperation.Less, false),
                (1, 2, NumberComparisonOperation.LessOrEquals, true),
                (2, 2, NumberComparisonOperation.LessOrEquals, true),
                (2, 1, NumberComparisonOperation.LessOrEquals, false),
                (1, 2, NumberComparisonOperation.Equals, false),
                (2, 2, NumberComparisonOperation.Equals, true),
                (2, 1, NumberComparisonOperation.Equals, false),
                (1, 2, NumberComparisonOperation.NotEquals, true),
                (2, 2, NumberComparisonOperation.NotEquals, false),
                (2, 1, NumberComparisonOperation.NotEquals, true),
                (1, 2, NumberComparisonOperation.More, false),
                (2, 2, NumberComparisonOperation.More, false),
                (2, 1, NumberComparisonOperation.More, true),
                (1, 2, NumberComparisonOperation.MoreOrEquals, false),
                (2, 2, NumberComparisonOperation.MoreOrEquals, true),
                (2, 1, NumberComparisonOperation.MoreOrEquals, true),
            ];

            foreach (var (firstOp, secondOp, operation, res) in ops)
            {
                var con = new NumberComparisonConverter()
                {
                    Operation = operation,
                    Operand = secondOp
                };

                bool actualResult = (bool)con.Convert(firstOp, typeof(double), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void ComparisonOperationWithManyDifferentConvertersTest()
        {
            double op1 = 10;
            double op2 = 20;
            Color color = Colors.Red;
            SolidColorBrush brush = new(color);

            var con = new NumberComparisonConverter()
            {
                Operation = NumberComparisonOperation.Equals,
                Operand = op2,
                Then = new BoolToBrushConverter()
                {
                    ForFalse = brush,
                    Then = new BrushToColorConverter()
                }
            };

            var actualResult = (Color)con.Convert(op1, typeof(double), null, CultureInfo.CurrentCulture);
            
            Assert.That(actualResult, Is.EqualTo(color));
        }

        [Test]
        public void ComparisonOperationWithInvalidValueTest()
        {
            string op1 = "string";
            double op2 = 4;

            var con = new NumberComparisonConverter()
            {
                Operation = NumberComparisonOperation.Less,
                Operand = op2
            };

            Assert.Catch<FormatException>(() => con.Convert(op1, typeof(double), null, CultureInfo.CurrentCulture));
        }
    }
}
