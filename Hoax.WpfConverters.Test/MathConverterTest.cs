using Hoax.WpfConverters;
using System.Collections.ObjectModel;
using System.Globalization;

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
            double op = 2.9;

            var subtractCon = new MathConverter()
            {
                Operation = MathOperation.Subtraction
            };

            Assert.Catch<ArgumentException>(() => subtractCon.Convert(op, typeof(double), null, CultureInfo.CurrentCulture));
        }

        [Test]
        public void MultiplyManyOperandsTest()
        {
            double op1 = 2;
            Collection<double> ops = [1.5, 3, 9.9, 2];

            var con = new MathConverter()
            {
                Operation = MathOperation.Multiply,
                Operands = ops,
            };

            double actualResult = Convert(con, op1);
            double expectedResult = op1;

            for (int i = 0; i < ops.Count; i++)
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
            Collection<double> ops = [75, 3, 5, 5];

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
        public void ModWithManyOperandsAndSecondConverterTest()
        {
            double op1 = 200;
            double op2 = 100;
            Collection<double> ops = [110, 70, 7];

            var con = new MathConverter()
            {
                Operation = MathOperation.Mod,
                Operands = ops,
                Then = new MathConverter()
                {
                    Operation = MathOperation.Division,
                    Operand = op2
                }
            };

            double actualResult = Convert(con, op1);
            double expectedResult = op1;

            for (int i = 0; i < ops.Count; i++)
            {
                expectedResult %= ops[i];
            }

            expectedResult /= op2;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void PowerWithManyDifferentConvertersTest()
        {
            double powerBase = 10;
            double powerValue = 2;
            double powerRes = Math.Pow(powerBase, powerValue);

            var con = new MathConverter()
            {
                Operation = MathOperation.Power,
                Operand = powerValue,
                Then = new NumberComparisonConverter()
                {
                    Operation = NumberComparisonOperation.Equals,
                    Operand = powerRes,
                    Then = new ObjectToStringConverter()
                }
            };

            string actualResult = (string)con.Convert(powerBase, typeof(double), null, CultureInfo.CurrentCulture);
            string expectedResult = (powerRes == 100).ToString();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void RoundWithManyOperandsTest()
        {
            double op1 = 2.9876;
            Collection<double> ops = [2, 5];

            var con = new MathConverter()
            {
                Operation = MathOperation.Round,
                Operands = ops,
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(Math.Round(op1, (int)ops[0])));
        }

        [Test]
        public void MaxWithManyOperandsAndManyConvertersWithManyOperandsTest()
        {
            double op1 = 2.9876;
            Collection<double> ops2 = [2, 5, 8.9];
            string format = "The max value is {0}";
            string separator = ".";

            var con = new MathConverter()
            {
                Operation = MathOperation.Max,
                Operands = ops2,
                Then = new StringFormatConverter()
                {
                    Format = format,
                    Then = new CollectionToStringConverter()
                    {
                        Separator = separator
                    }
                }
            };

            string actualResult = (string)con.Convert(op1, typeof(string), null, CultureInfo.CurrentCulture);

            Collection<double> generalCollection = [op1, .. ops2];
            double max = generalCollection.Max();
            string formattedString = string.Format(format, max);
            string expectedResult = string.Join<char>(separator, formattedString);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
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
        public void AbsoluteWithSecondConverterTest()
        {
            double op1 = -2.9876;
            double op2 = -5.943;

            var con = new MathConverter()
            {
                Operation = MathOperation.Absolute,
                Then = new MathConverter()
                {
                    Operation = MathOperation.Multiply,
                    Operand = op2
                }
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(Math.Abs(op1) * op2));
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
            Collection<double> ops = [1, 2, 3];

            var con = new MathConverter()
            {
                Operation = MathOperation.Increment,
                Operands = ops
            };

            double actualResult = Convert(con, op1);

            Assert.That(actualResult, Is.EqualTo(++op1));
        }

        [Test]
        public void DecrementWithInvalidInputValueTest()
        {
            string op = "string";

            var con = new MathConverter()
            {
                Operation = MathOperation.Decrement,
            };

            Assert.Catch<FormatException>(() => con.Convert(op, typeof(string), null, CultureInfo.CurrentCulture));
        }

        private double Convert(MathConverter converter, double operand) => (double)converter.Convert(operand, typeof(double), null, CultureInfo.CurrentCulture);
    }
}