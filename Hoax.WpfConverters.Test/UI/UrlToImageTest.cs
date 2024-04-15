using NUnit.Framework.Internal;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Hoax.WpfConverters.Test.UI
{
    internal class UrlToImageTest
    {
        private const int ExpectedHeight = 100;
        private const int ExpectedWidth = 100;

        private readonly string _stringUrl = "https://en.wikipedia.org/static/images/icons/wikipedia.png";
        private readonly Uri? _expectedUri;

        public UrlToImageTest()
        {
            _expectedUri = new Uri(_stringUrl);
        }

        [Test]
        public void StringUrlToImageTest() => TestConvert(_stringUrl);

        [Test]
        public void UriToImageTest() => TestConvert(_expectedUri!);

        [Test]
        public void InvalidUrlToImageTest()
        {
            var con = new UrlToImageConverter();
            var result = con.Convert(null, typeof(object), null, CultureInfo.CurrentCulture);

            Assert.That(result, Is.EqualTo(DependencyProperty.UnsetValue));
        }

        private void TestConvert(object inputUrl)
        {
            var con = new UrlToImageConverter();
            var result = (BitmapImage)con.Convert(inputUrl, typeof(object), null, CultureInfo.CurrentCulture);

            Assert.That(result.UriSource.AbsoluteUri, Is.EqualTo(_expectedUri!.AbsoluteUri));

            if (result.IsDownloading)
            {
                result.DownloadCompleted += OnDownloadCompleted;
                result.DownloadFailed += OnDownloadFailed;
            }
            else
            {
                CheckBitmap(result);
            }
        }

        private void OnDownloadCompleted(object? sender, EventArgs e)
        {
            var bi = UnsubscribeEvents(sender);
            CheckBitmap(bi);
        }

        private void CheckBitmap(BitmapImage? bi)
        {
            Assert.Multiple(() =>
            {
                Assert.That(bi?.Height, Is.EqualTo(ExpectedHeight));
                Assert.That(bi?.Width, Is.EqualTo(ExpectedWidth));
            });
        }

        private void OnDownloadFailed(object? sender, System.Windows.Media.ExceptionEventArgs e)
        {
            UnsubscribeEvents(sender);
            Assert.Fail(e.ErrorException.Message);
        }

        private BitmapImage? UnsubscribeEvents(object? sender)
        {
            var bi = (BitmapImage?)sender;

            if (bi is null)
            {
                Assert.Fail("Unable to cast sender to Bitmap");
                return null;
            }

            bi.DownloadCompleted -= OnDownloadCompleted;
            bi.DownloadFailed -= OnDownloadFailed;

            return bi;
        }
    }
}
