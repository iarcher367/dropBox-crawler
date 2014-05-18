namespace Crawler.Test
{
    using Business;
    using Business.DataAccess;
    using Business.Engines;
    using Business.Models;
    using Moq;
    using NUnit.Framework;

    public class DropBoxManagerTest
    {
        private IDropBoxManager _sut;
        private Mock<IDropBoxProxy> _dropBoxProxy;
        private Mock<IOAuthEngine> _oAuthEngine;
        private Mock<IAnalyticsEngine> _analyticsEngine;

        [SetUp]
        public void Setup()
        {
            _analyticsEngine = new Mock<IAnalyticsEngine>();
            _dropBoxProxy = new Mock<IDropBoxProxy>();
            _oAuthEngine = new Mock<IOAuthEngine>();

            _sut = new DropBoxManager(_dropBoxProxy.Object, _oAuthEngine.Object, _analyticsEngine.Object);
        }

        [Test]
        public void ShouldBuildAuthorizeUrl()
        {
            const string url = "https://www.dropbox.com/1/oauth2/authorize?response_type=code" +
                "&client_id=75zoqoklvaz1chg&redirect_uri=http://localhost";

            _oAuthEngine.Setup(x => x.BuildAuthorizeUrl()).Returns(url);

            var actual = _sut.BuildAuthorizeUrl();

            Assert.That(actual, Is.EqualTo(url));

            _oAuthEngine.VerifyAll();
        }

        [Test]
        public void ShouldAcquireBearerToken()
        {
            const string code = "qRqFd2ecRK4AAAAAAAAAvY9a4V3r2L89mpyYlX0bkuE";
            const string token = "oesUCS8O0E2408OHISO0kdo08idoed9EO9bknoe08if9";

            _dropBoxProxy.Setup(x => x.PostToken(code)).Returns(token);

            var actual = _sut.AcquireBearerToken(code);

            Assert.That(actual, Is.EqualTo(token));

            _dropBoxProxy.VerifyAll();
        }

        [Test]
        public void ShouldCrawlToken()
        {
            const string token = "oesUCS8O0E2408OHISO0kdo08idoed9EO9bknoe08if9";
            const string report = "File Count: 45\nFolder Count: 44\nTop 5 Mime Types: text/csv (45)";

            _analyticsEngine.Setup(x => x.GenerateReport(It.IsAny<Analytics>())).Returns(report);

            var actual = _sut.Crawl(token);

            Assert.That(actual, Is.EqualTo(report));

            _analyticsEngine.VerifyAll();
        }
    }
}
