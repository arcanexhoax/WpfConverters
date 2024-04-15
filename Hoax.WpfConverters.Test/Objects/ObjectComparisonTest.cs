using System.Globalization;

namespace Hoax.WpfConverters.Test.Objects
{
    internal class ObjectComparisonTest
    {
        [Test]
        public void SimpleComparisonTest()
        {
            List<(object? firstOp, object? secondOp, ObjectComparisonOperation operation, bool result)> ops = 
            [
                (1, 1, ObjectComparisonOperation.Equals, true),
                (1, 2, ObjectComparisonOperation.Equals, false),
                ("1", "1", ObjectComparisonOperation.Equals, true),
                (new object(), new object(), ObjectComparisonOperation.Equals, false),
                (null, new object(), ObjectComparisonOperation.Equals, false),
                (5, null, ObjectComparisonOperation.Equals, false),
                (null, null, ObjectComparisonOperation.Equals, true),
                ("1", "1", ObjectComparisonOperation.NotEquals, false),
                (1, 1, ObjectComparisonOperation.NotEquals, false),
                (1, 2, ObjectComparisonOperation.NotEquals, true),
                (new object(), new object(), ObjectComparisonOperation.NotEquals, true),
                (null, 5, ObjectComparisonOperation.NotEquals, true),
                (new object(), null, ObjectComparisonOperation.NotEquals, true),
                (null, null, ObjectComparisonOperation.NotEquals, false),
                (2, null, ObjectComparisonOperation.IsNull, false),
                (new object(), null, ObjectComparisonOperation.IsNull, false),
                (null, null, ObjectComparisonOperation.IsNull, true),
                (null, null, ObjectComparisonOperation.IsNotNull, false),
                (1, null, ObjectComparisonOperation.IsNotNull, true),
                ("1", null, ObjectComparisonOperation.IsNotNull, true),
            ];

            foreach (var (firstOp, secondOp, operation, res) in ops)
            {
                var con = new ObjectComparisonConverter()
                {
                    Operation = operation,
                    Operand = secondOp
                };

                bool actualResult = (bool)con.Convert(firstOp, typeof(object), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void ComparisonWithManyDifferentConvertersTest()
        {
            object? op1 = null;
            double op2 = 5;
            object op3 = 5;
            bool expectedResult = true;

            var con = new ObjectComparisonConverter()
            {
                Operation = ObjectComparisonOperation.IsNull,
                Then = new BoolToDoubleConverter()
                {
                    ForTrue = op2,
                    Then = new ObjectComparisonConverter()
                    {
                        Operation = ObjectComparisonOperation.Equals,
                        Operand = op3
                    }
                }
            };

            bool actualResult = (bool)con.Convert(op1, typeof(object), null, CultureInfo.CurrentCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}
