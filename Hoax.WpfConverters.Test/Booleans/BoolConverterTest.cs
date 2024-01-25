using System.Globalization;

namespace Hoax.WpfConverters.Test.Booleans
{
    internal class BoolConverterTest
    {
        [Test]
        public void SimpleBoolOperationsTest()
        {
            List<(bool Op1, bool Op2, BoolOperation Operation)> ops = [
                (true, false, BoolOperation.And),    
                (true, true, BoolOperation.And),    
                (false, false, BoolOperation.And),    
                (false, false, BoolOperation.Or),    
                (true, false, BoolOperation.Or),    
                (true, true, BoolOperation.Or),    
                (true, true, BoolOperation.Equals),    
                (true, false, BoolOperation.Equals),    
                (false, false, BoolOperation.Equals),    
                (false, false, BoolOperation.NotEquals),    
                (false, true, BoolOperation.NotEquals),    
                (true, true, BoolOperation.NotEquals),    
                (true, true, BoolOperation.Xor),    
                (true, false, BoolOperation.Xor),    
                (false, false, BoolOperation.Xor),    
                (true, false, BoolOperation.Not),    
                (false, false, BoolOperation.Not),    
            ];

            foreach (var (op1, op2, operation) in ops )
            {
                var con = new BoolConverter()
                {
                    Operation = operation,
                    Operand = op2
                };

                bool actualResult = (bool)con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture);
                bool expectedResult = operation switch
                {
                    BoolOperation.Not => !op1 ,
                    BoolOperation.And => op1 && op2,
                    BoolOperation.Or => op1 || op2,
                    BoolOperation.Xor => op1 ^ op2,
                    BoolOperation.NotEquals => op1 != op2,
                    _ => op1 == op2,
                };

                Assert.That(actualResult, Is.EqualTo(expectedResult));
            }
        }

        [Test]
        public void ComparisonWithManyDifferentConvertersTest()
        {
            bool op1 = true;
            bool op2 = false;
            double op3 = 10;

            var con = new BoolConverter()
            {
                Operation = BoolOperation.And,
                Operand = op2,
                Then = new BoolToDoubleConverter()
                {
                    ForFalse = op3,
                    Then = new ObjectToStringConverter()
                }
            };

            string actualResult = (string)con.Convert(op1, typeof(string), null, CultureInfo.CurrentCulture);
            string expectedResult = op3.ToString();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ComparisonWithInvalidInputValueTest()
        {
            string op1 = "string";

            var con = new BoolConverter()
            {
                Operation = BoolOperation.Not
            };

            Assert.Catch<FormatException>(() => con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture));
        }
    }
}
