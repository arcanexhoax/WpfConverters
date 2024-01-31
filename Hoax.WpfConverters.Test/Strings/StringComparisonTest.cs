using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoax.WpfConverters.Test.Strings
{
    internal class StringComparisonTest
    {
        [Test]
        public void SimpleComparisonTest()
        {
            List<(string? operand1, StringComparisonOperation operation, string? operand2, bool result)> values = [
                ("string", StringComparisonOperation.Equals, "string", true),    
                ("string", StringComparisonOperation.Equals, "STRING", false),    
                ("string", StringComparisonOperation.Equals, "123", false),    
                (null, StringComparisonOperation.Equals, "123", false),    
                ("string", StringComparisonOperation.Equals, null, false),    
                (null, StringComparisonOperation.Equals, null, true),    
                ("string", StringComparisonOperation.EqualsIgnoreCase, "string", true),    
                ("string", StringComparisonOperation.EqualsIgnoreCase, "STRING", true),    
                ("string", StringComparisonOperation.EqualsIgnoreCase, "123", false),    
                (null, StringComparisonOperation.EqualsIgnoreCase, "123", false),    
                ("string", StringComparisonOperation.EqualsIgnoreCase, null, false),    
                (null, StringComparisonOperation.EqualsIgnoreCase, null, true),    
                ("string", StringComparisonOperation.NotEquals, "string", false),    
                ("string", StringComparisonOperation.NotEquals, "STRING", true),    
                ("string", StringComparisonOperation.NotEquals, "123", true),    
                (null, StringComparisonOperation.NotEquals, "123", true),    
                ("string", StringComparisonOperation.NotEquals, null, true),    
                (null, StringComparisonOperation.NotEquals, null, false),    
                ("string", StringComparisonOperation.NotEqualsIgnoreCase, "string", false),    
                ("string", StringComparisonOperation.NotEqualsIgnoreCase, "STRING", false),    
                ("string", StringComparisonOperation.NotEqualsIgnoreCase, "123", true),    
                (null, StringComparisonOperation.NotEqualsIgnoreCase, "123", true),    
                ("string", StringComparisonOperation.NotEqualsIgnoreCase, null, true),    
                (null, StringComparisonOperation.NotEqualsIgnoreCase, null, false),    
                ("string", StringComparisonOperation.IsEmpty, null, false),    
                (string.Empty, StringComparisonOperation.IsEmpty, null, true),    
                (null, StringComparisonOperation.IsEmpty, null, false),    
                (null, StringComparisonOperation.IsNull, null, true),    
                ("string", StringComparisonOperation.IsNull, null, false),    
                (string.Empty, StringComparisonOperation.IsNull, null, false),    
                (string.Empty, StringComparisonOperation.IsNullOrEmpty, null, true),    
                (null, StringComparisonOperation.IsNullOrEmpty, null, true),    
                ("string", StringComparisonOperation.IsNullOrEmpty, null, false),    
            ];

            foreach (var (op1, operation, op2, res) in values)
            {
                var con = new StringComparisonConverter()
                {
                    Operation = operation, 
                    Operand = op2
                };

                bool actualResult = (bool)con.Convert(op1, typeof(string), null, CultureInfo.InvariantCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void ComparisonWithManyDifferentConvertersTest()
        {
            string? op1 = null;
            int op2 = 2;
            int op3 = 5;
            double expectedResult = 7;

            var con = new StringComparisonConverter()
            {
                Operation = StringComparisonOperation.IsNull,
                Then = new BoolToDoubleConverter()
                {
                    ForTrue = op2,
                    Then = new MathConverter()
                    {
                        Operation = MathOperation.Addition,
                        Operand = op3,
                    }
                }
            };

            double actualResult = (double)con.Convert(op1, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ComparisonWithInvalidInputTest()
        {
            bool op1 = true;
            string op2 = "true";
            bool expectedResult = true;

            var con = new StringComparisonConverter()
            {
                Operation = StringComparisonOperation.EqualsIgnoreCase,
                Operand = op2
            };

            bool actualResult = (bool)con.Convert(op1, typeof(bool), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
