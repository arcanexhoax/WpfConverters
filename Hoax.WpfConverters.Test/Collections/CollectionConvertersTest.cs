using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Hoax.WpfConverters.Test.Collections
{
    internal class CollectionConvertersTest
    {
        [Test]
        public void SimpleCollectionToCountTest()
        {
            List<(IEnumerable Collection, double Result)> values = [
                (new Collection<int>() { 1, 3, 4, 15 }, 4),
                (new int[] { 5, 8, 6, 4, 2, 4, 8, 10, 1 }, 9),
                ("string", 6)
            ];
            
            foreach (var (col, res) in values)
            {
                var con = new CollectionToCountConverter();
                int actualResult = (int)con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToCountWithManyDifferentConverters()
        {
            List<(IEnumerable Collection, int Operand, bool Result)> values = [
                (new List<int>() { 1, 5, 3, 5 }, 2, true),
                (new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 1, false),
                ("1234567890123456", 15, true)
            ];

            foreach (var (col, op, res) in values)
            {
                var con = new CollectionToCountConverter()
                {
                    Then = new MathConverter()
                    {
                        Operation = MathOperation.Sqrt,
                        Then = new NumberComparisonConverter()
                        {
                            Operation = NumberComparisonOperation.LessOrEquals,
                            Operand = op
                        }
                    }
                };

                bool actualResult = (bool)con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToCountWithInvalidInputTest()
        {
            int op = 2;

            var con = new CollectionToCountConverter();

            Assert.Catch<ArgumentException>(() => con.Convert(op, typeof(IEnumerable), null, CultureInfo.CurrentCulture));
        }
    }
}
