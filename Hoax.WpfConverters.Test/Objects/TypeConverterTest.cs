using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoax.WpfConverters.Test.Objects
{
    internal class TypeConverterTest
    {
        [Test]
        public void SimpleTypeConvertersTest()
        {
            List<(object? operand, Type to, object? result)> values = [
                (1, typeof(double), 1d),
                (1, typeof(char), '\u0001'),
                (1, typeof(bool), true),
                (0, typeof(bool), false),
                (0, typeof(string), "0"),
                (0.3d, typeof(int), 0),
                (0.3d, typeof(bool), true),
                (0d, typeof(bool), false),
                (0.3d, typeof(string), "0.3"),
                (0.3f, typeof(string), "0.3"),
                (0.3m, typeof(string), "0.3"),
                ('1', typeof(int), 49),
                ('1', typeof(double), 49d),
                ('1', typeof(float), 49f),
                ('1', typeof(decimal), 49m),
                ('0', typeof(string), "0"),
                (false, typeof(int), 0),
                (true, typeof(int), 1),
                (true, typeof(double), 1d),
                (false, typeof(double), 0d),
                (true, typeof(string), "True"),
                (false, typeof(string), "False"),
                ("1", typeof(int), 1),
                ("1.5", typeof(double), 1.5d),
                ("1", typeof(char), '1'),
                ("1", typeof(bool), true),
                (string.Empty, typeof(bool), false),
                (null, typeof(int), 0),
                (null, typeof(double), 0d),
                (null, typeof(bool), false),
                (null, typeof(char), '\0'),
                (null, typeof(string), null),
            ];

            foreach (var (op, to, res) in values)
            {
                var con = new TypeConverter()
                {
                    To = to,
                };

                var actualResult = con.Convert(op, typeof(object), null, CultureInfo.InvariantCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void ConvertTypeWithDifferentManyConvertersTest()
        {
            int op1 = 1;
            int op2 = 2;    
            Type boolType = typeof(bool);
            char expectedResult = 'r';

            var con = new TypeConverter()
            {
                To = boolType, 
                Then = new BoolToStringConverter()
                {
                    ForTrue = "string",
                    Then = new CollectionToItemConverter()
                    {
                        Index = op2
                    }
                }
            };

            char actualResult = (char)con.Convert(op1, typeof(int), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
