using System.Globalization;

namespace Hoax.WpfConverters.Test.Objects
{
    internal class ObjectToStringTest
    {
        [Test]
        public void SimpleObjectToStringTest()
        {
            List<(object? operand, NullHanding nullHandling, string? result)> values = 
            [
                (new object(), default, "System.Object"),
                (123, default, "123"),
                (false, default, "False"),
                ('c', NullHanding.ReturnEmptyString, "c"),
                ("string", NullHanding.ReturnNullAsString, "string"),
                (1.2, NullHanding.ReturnNull, "1,2"),
                (string.Empty, default, string.Empty),
                (null, NullHanding.ReturnNull, null),
                (null, NullHanding.ReturnNullAsString, "Null"),
                (null, NullHanding.ReturnEmptyString, string.Empty)
            ];

            foreach (var (op, operation, res) in values)
            {
                var con = new ObjectToStringConverter()
                {
                    NullHanding = operation
                };

                string? actualResult = (string?)con.Convert(op, typeof(object), null, CultureInfo.InvariantCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void ObjectToStringWithManyDifferentConvertersTest()
        {
            object op = new();
            int expectedResult = 13;

            var con = new ObjectToStringConverter()
            {
                Then = new StringCaseConverter()
                {
                    Operation = CaseOperation.ToUpper,
                    Then = new CollectionToCountConverter()
                }
            };

            int actualResult = (int)con.Convert(op, typeof(object), null, CultureInfo.CurrentCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
