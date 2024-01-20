using System.Globalization;
using WpfConverters.Converters;

namespace WpfConverters.Test
{
    public class MathConvertersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleAdditionTest()
        {
            double op1 = 2.9;
            double op2 = 4.8;

            var con = new MathConverter()
            {
                Operation = MathOperation.Addition,
                Operand = op2,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(op1 + op2));
        }

        [Test]
        public void SubtractionWithOneOperandTest()
        {
            double op1 = 2.9;
            double op2 = 4.8;

            var con = new MathConverter()
            {
                Operation = MathOperation.Subtraction,
                Operand = op2,
                IgnoreBindingValue = true
            };

            Assert.Catch<ArgumentException>(() => con.Convert(op1, typeof(double), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void MultiplyManyOperandsTest()
        {
            double op1 = 2;
            double[] ops = [1.5, 3, 9.9, 2];

            var con = new MathConverter()
            {
                Operation = MathOperation.Multiply,
                Operands = ops,
            };

            double actualResult = Convert(con, op1);
            double expectedResult = op1;

            for (int i = 0; i < ops.Length; i++)
            {
                expectedResult *= ops[i];
            }

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void DivisionWithSetOperandAndOperandsTest()
        {
            double op1 = 56;
            double op2 = 2;
            double[] ops = [75, 3, 5, 5];

            var con = new MathConverter()
            {
                Operation = MathOperation.Division,
                Operands = ops,
                Operand = op2,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(op1 / op2));
        }

        [Test]
        public void ModWithManyOperandsAndIgnoreBindingValueTest()
        {
            double op1 = 200;
            double[] ops = [110, 70, 7];

            var con = new MathConverter()
            {
                Operation = MathOperation.Mod,
                Operands = ops,
                IgnoreBindingValue = true
            };

            double actualResult = Convert(con, op1);
            double expectedResult = ops[0];

            for (int i = 1; i < ops.Length; i++)
            {
                expectedResult %= ops[i];
            }

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void PowerWithNoOperandsAndIgnoreBindingValueTest()
        {
            var con = new MathConverter()
            {
                Operation = MathOperation.Power,
                IgnoreBindingValue = true
            };

            Assert.Catch<ArgumentException>(() => con.Convert(0, typeof(double), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void RoundWithManyOperandsTest()
        {
            double op1 = 2.9876;
            double[] ops = [2, 5];

            var con = new MathConverter()
            {
                Operation = MathOperation.Round,
                Operands = ops,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(Math.Round(op1, (int)ops[0])));
        }

        [Test]
        public void MaxWithManyOperandsAndIgnoreBindingValueTest()
        {
            double op1 = 2.9876;
            double[] ops = [2, 5, 8.9];

            var con = new MathConverter()
            {
                Operation = MathOperation.Max,
                Operands = ops,
                IgnoreBindingValue = true
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(ops.Max()));
        }

        [Test]
        public void MinWithNoOperandsTest()
        {
            double op1 = 2.9876;

            var con = new MathConverter()
            {
                Operation = MathOperation.Min,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(op1));
        }

        [Test]
        public void AbsoluteWithIgnoreBindingValueTest()
        {
            double op1 = -2.9876;
            double op2 = -5.943;

            var con = new MathConverter()
            {
                Operation = MathOperation.Absolute,
                Operand = op2,
                IgnoreBindingValue = true
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(Math.Abs(op2)));
        }

        [Test]
        public void SqrtWithOnlyBindingOperandTest()
        {
            double op1 = 25;

            var con = new MathConverter()
            {
                Operation = MathOperation.Sqrt,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(Math.Sqrt(op1)));
        }

        [Test]
        public void IncrementWithManyOperandsTest()
        {
            double op1 = 25.123;
            double[] ops = [1, 2, 3];

            var con = new MathConverter()
            {
                Operation = MathOperation.Increment,
                Operands = ops
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(++op1));
        }

        [Test]
        public void DecrementWithManyOperandsAndIgnoreBindingValueTest()
        {
            double op1 = 25.123;
            double[] ops = [1, 2, 3];

            var con = new MathConverter()
            {
                Operation = MathOperation.Increment,
                Operands = ops,
                IgnoreBindingValue = true
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(++ops[0]));
        }

        private double Convert(MathConverter converter, double operand) => (double)converter.Convert(operand, typeof(double), null, CultureInfo.CurrentCulture);
    }
}