using System.Globalization;

namespace Hoax.WpfConverters.Test.Strings
{
    internal class StringCaseTest
    {
        [Test]
        public void SimpleStringCaseTest()
        {
            List<(string? operand, CaseOperation operation, string? result)> values = [
                ("string", CaseOperation.ToUpper, "STRING"),    
                ("sTrInG", CaseOperation.ToUpperFirstLetterAndToLowerOther, "String"),    
                ("sTrInG", CaseOperation.ToUpperFirstLetterAndIgnoreOther, "STrInG"),
                ("sTrInG", CaseOperation.ToLower, "string"),
                ("sTrInG", CaseOperation.Invert, "StRiNg"),
                (string.Empty, CaseOperation.ToUpper, string.Empty),
                (null, CaseOperation.ToLower, null),
            ];

            foreach (var (op, operation, res) in values)
            {
                var con = new StringCaseConverter()
                {
                    Operation = operation
                };

                string? actualResult = (string?)con.Convert(op, typeof(string), null, CultureInfo.InvariantCulture);

                Assert.That(res, Is.EqualTo(actualResult));
            }
        }

        [Test]
        public void StringCaseWithManyDifferentConvertersTest()
        {
            string op1 = "string";
            int op2 = 1;
            string op3 = "T";

            var con = new StringCaseConverter()
            {
                Operation = CaseOperation.ToUpper,
                Then = new CollectionToItemConverter()
                {
                    Index = op2,
                    Then = new StringComparisonConverter()
                    {
                        Operation = StringComparisonOperation.Equals,
                        Operand = op3
                    }
                }
            };

            bool actualResult = (bool)con.Convert(op1, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.True);
        }

        [Test]
        public void StringCaseWithInvalidInputTest()
        {
            bool op = false;
            string expectedResult = "FALSE";

            var con = new StringCaseConverter()
            {
                Operation = CaseOperation.ToUpper
            };

            var actualResult = con.Convert(op, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
