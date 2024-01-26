using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.Collections
{
    internal class CollectionToItemConvertersTest
    {
        [Test]
        public void SimpleCollectionToItemTest()
        {
            List<(IEnumerable Collection, int Index, object Result)> values = [
                (new Collection<Color>() { Colors.White, Colors.Wheat, Colors.SeaShell, Colors.Aquamarine }, 2, Colors.SeaShell),
                (new int[] { 5, 8, 6, 4, 2, 4, 8, 10, 1 }, 5, 4),
                ("string", 1, 't')
            ];
            
            foreach (var (col, ind, res) in values)
            {
                var con = new CollectionToItemConverter() 
                { 
                    Index = ind 
                };
                var actualResult = con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToItemWithManyDifferentConverters()
        {
            List<(IEnumerable Collection, int Index, string Operand, bool Result)> values = [
                (new List<int>() { 1, 5, 3, 5 }, 2, "3", true),
                (new object[] { false, true, true, true }, 1, "True", true),
                ("1234567890123456", 5, "5", false)
            ];

            foreach (var (col, ind, op, res) in values)
            {
                var con = new CollectionToItemConverter()
                {
                    Index = ind,
                    Then = new ObjectToStringConverter()
                    {
                        Then = new StringComparisonConverter()
                        {
                            Operation = StringComparisonOperation.Equals,
                            Operand = op
                        }
                    }
                };

                bool actualResult = (bool)con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToItemWithInvalidInputTest()
        {
            int op = 2;

            var con = new CollectionToItemConverter();

            Assert.Catch<ArgumentException>(() => con.Convert(op, typeof(IEnumerable), null, CultureInfo.CurrentCulture));
        }
    }
}
