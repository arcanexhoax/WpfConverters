using System.Globalization;

namespace Hoax.WpfConverters.Test.Booleans
{
    internal class BoolConverterTest
    {
        [Test]
        public void SimpleBoolOperationsTest()
        {
            List<(bool Op1, bool Op2, BoolOperation Operation, bool Result)> ops = 
            [
                (true, false, BoolOperation.And, false),    
                (true, true, BoolOperation.And, true),    
                (false, false, BoolOperation.And, false),    
                (false, false, BoolOperation.Or, false),    
                (true, false, BoolOperation.Or, true),    
                (true, true, BoolOperation.Or, true),    
                (true, true, BoolOperation.Equals, true),    
                (true, false, BoolOperation.Equals, false),    
                (false, false, BoolOperation.Equals, true),    
                (false, false, BoolOperation.NotEquals, false),    
                (false, true, BoolOperation.NotEquals, true),    
                (true, true, BoolOperation.NotEquals, false),    
                (true, true, BoolOperation.Xor, false),    
                (true, false, BoolOperation.Xor, true),    
                (false, false, BoolOperation.Xor, false),    
                (true, false, BoolOperation.Not, false),    
                (false, false, BoolOperation.Not, true),    
            ];

            foreach (var (op1, op2, operation, res) in ops )
            {
                var con = new BoolConverter()
                {
                    Operation = operation,
                    Operand = op2
                };

                bool actualResult = (bool)con.Convert(op1, typeof(bool), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
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
