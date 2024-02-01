using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoax.WpfConverters.Test.Strings
{
    internal class StringFormatTest
    {
        [Test]
        public void SimpleFormatTest()
        {
            List<(string? format, object? arg, string? result)> values = [
                ("{0}string", null, "string"),
                (null, 1, null),
                (null, null, null),
                ("{0}string", 1, "1string"),
                ("string{0}", false, "stringFalse"),
                ("stri {0} ng", 2.5, "stri 2,5 ng"),
                ("stri {0} ng", new object(), "stri System.Object ng"),
                ("{1} stri {0} ng {2}", new List<int> { 1, 2, 3 }, "2 stri 1 ng 3"),
                ("{1} stri {0} ng {2}", new List<bool> { false, true, true }, "True stri False ng True"),
                ("{1} stri {0} ng {2}", new List<double> { 1.2332, 0.57566, 2145.2 }, "0,57566 stri 1,2332 ng 2145,2"),
                ("{1} stri {0} ng {2}", new List<object> { new(), new(), new() }, "System.Object stri System.Object ng System.Object"),
            ];

            foreach (var (input, arg, res) in values )
            {
                var con = new StringFormatConverter()
                {
                    Format = input
                };

                string? actualResult = (string)con.Convert(arg, typeof(string), null, CultureInfo.InvariantCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void FormatWithManyDifferentConvertersTest()
        {
            bool op = false;
            string format = "{0} - format";
            int index = 0;
            char expectedResult = 'f';

            var con = new StringFormatConverter()
            {
                Format = format,
                Then = new StringCaseConverter()
                {
                    Operation = CaseOperation.Invert,
                    Then = new CollectionToItemConverter()
                    {
                        Index = index
                    }
                }
            };

            char actualResult = (char)con.Convert(op, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
