using System.Globalization;
using System.Windows;

namespace Hoax.WpfConverters.Test.UI
{
    internal class VisibilityToBoolTest
    {
        [Test]
        public void SimpleVisibilityToBoolTest()
        {
            List<(Visibility vis, bool forVisible, bool forHidden, bool forCollapsed, bool result)> values =
            [
                (Visibility.Visible, true, false, false, true),
                (Visibility.Visible, true, true, true, true),
                (Visibility.Visible, false, true, true, false),
                (Visibility.Visible, false, false, false, false),
                (Visibility.Hidden, false, true, false, true),
                (Visibility.Hidden, true, true, true, true),
                (Visibility.Hidden, true, false, true, false),
                (Visibility.Hidden, false, false, false, false),
                (Visibility.Collapsed, false, false, true, true),
                (Visibility.Collapsed, true, true, true, true),
                (Visibility.Collapsed, true, true, false, false),
                (Visibility.Collapsed, false, false, false, false),
            ];

            foreach (var (vis, forVis, forHid, forCol, res) in values)
            {
                var con = new VisibilityToBoolConverter() 
                {
                    ForVisible = forVis,
                    ForHidden = forHid,
                    ForCollapsed = forCol
                };

                bool actualResult = (bool)con.Convert(vis, typeof(bool), null, CultureInfo.InvariantCulture);

                Assert.That(actualResult, Is.EqualTo(res));
            }
        }

        [Test]
        public void MultipleVisibleToBoolTest()
        {
            Visibility inputVis = Visibility.Collapsed;
            bool forCollapsed = true;
            Visibility forTrue = Visibility.Visible;
            Visibility expectedResult = Visibility.Visible;

            var con = new VisibilityToBoolConverter()
            {
                ForCollapsed = forCollapsed,
                Then = new BoolToVisibilityConverter()
                {
                    ForTrue = forTrue
                }
            };

            var actualResult = (Visibility)con.Convert(inputVis, typeof(Visibility), null, CultureInfo.InvariantCulture);

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void VisibilityToBoolWithInvalidInputTest()
        {
            var con = new VisibilityToBoolConverter();

            Assert.Catch<ArgumentException>(() => con.Convert(null, typeof(Visibility), null, CultureInfo.InvariantCulture));
        }
    }
}
