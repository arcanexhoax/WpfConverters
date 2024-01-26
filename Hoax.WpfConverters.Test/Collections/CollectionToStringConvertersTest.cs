using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Media;

namespace Hoax.WpfConverters.Test.Collections
{
    internal class CollectionToStringConvertersTest
    {
        [Test]
        public void SimpleCollectionToStringTest()
        {
            List<(IEnumerable Collection, string Separator, string Result)> values = [
                (new Collection<Color>() { Colors.White, Colors.Wheat }, string.Empty, "#FFFFFFFF#FFF5DEB3"),
                (new int[] { 5, 8, 6, 4 }, ";", "5;8;6;4"),
                ("string", " ", "s t r i n g")
            ];
            
            foreach (var (col, sep, res) in values)
            {
                var con = new CollectionToStringConverter() 
                { 
                    Separator = sep
                };
                var actualResult = con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToStringWithManyDifferentConverters()
        {
            List<(IEnumerable Collection, string Separator, int Operand, bool Result)> values = [
                (new List<int>() { 1, 5, 3, 5 }, ".", 7, true),
                (new object[] { false, true, true, true }, "true", 29, true),
                ("1234567890123456", string.Empty, 15, false)
            ];

            foreach (var (col, sep, op, res) in values)
            {
                var con = new CollectionToStringConverter()
                {
                    Separator = sep,
                    Then = new CollectionToCountConverter()
                    {
                        Then = new NumberComparisonConverter()
                        {
                            Operation = NumberComparisonOperation.Equals,
                            Operand = op
                        }
                    }
                };

                bool actualResult = (bool)con.Convert(col, typeof(IEnumerable), null, CultureInfo.CurrentCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void CollectionToStringWithInvalidInputTest()
        {
            int op = 2;

            var con = new CollectionToItemConverter();

            Assert.Catch<ArgumentException>(() => con.Convert(op, typeof(IEnumerable), null, CultureInfo.CurrentCulture));
        }
    }
}
