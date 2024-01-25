using System.Globalization;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.Numbers
{
    internal class NumberComparisonTest
    {
        [Test]
        public void SimpleComparisonOperationsTest()
        {
            List<(double FirstOp, double SecondOp, NumberComparisonOperation Operation)> ops = [
                (1, 2, NumberComparisonOperation.Less),
                (2, 2, NumberComparisonOperation.Less),
                (2, 1, NumberComparisonOperation.Less),
                (1, 2, NumberComparisonOperation.LessOrEquals),
                (2, 2, NumberComparisonOperation.LessOrEquals),
                (2, 1, NumberComparisonOperation.LessOrEquals),
                (1, 2, NumberComparisonOperation.Equals),
                (2, 2, NumberComparisonOperation.Equals),
                (2, 1, NumberComparisonOperation.Equals),
                (1, 2, NumberComparisonOperation.NotEquals),
                (2, 2, NumberComparisonOperation.NotEquals),
                (2, 1, NumberComparisonOperation.NotEquals),
                (1, 2, NumberComparisonOperation.More),
                (2, 2, NumberComparisonOperation.More),
                (2, 1, NumberComparisonOperation.More),
                (1, 2, NumberComparisonOperation.MoreOrEquals),
                (2, 2, NumberComparisonOperation.MoreOrEquals),
                (2, 1, NumberComparisonOperation.MoreOrEquals),
            ];

            foreach (var op in ops)
            {
                var con = new NumberComparisonConverter()
                {
                    Operation = op.Operation,
                    Operand = op.SecondOp
                };

                bool actualResult = (bool)con.Convert(op.FirstOp, typeof(double), null, CultureInfo.CurrentCulture);
                bool expectedResult = op.Operation switch
                {
                    NumberComparisonOperation.Less         => op.FirstOp < op.SecondOp,
                    NumberComparisonOperation.LessOrEquals => op.FirstOp <= op.SecondOp,
                    NumberComparisonOperation.More         => op.FirstOp > op.SecondOp,
                    NumberComparisonOperation.MoreOrEquals => op.FirstOp >= op.SecondOp,
                    NumberComparisonOperation.NotEquals    => op.FirstOp != op.SecondOp,
                    _                                      => op.FirstOp == op.SecondOp,
                };

                Assert.That(actualResult, Is.EqualTo(expectedResult));
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
