namespace Crawler.Test.DataAccess
{
    using Business;
    using Business.DataAccess;
    using Business.Rest;
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class DropBoxProxyTest
    {
        private IDropBoxProxy _sut;
        private Mock<IConfig> _config;
        private Mock<IRestProxy> _restProxy;

        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfig>();
            _restProxy = new Mock<IRestProxy>();
            _sut = new DropBoxProxy(_restProxy.Object, _config.Object);
        }

        [Test]
        public void ShouldGetMetadataForPath()
        {
            const string resource = "/1/metadata/{root}/{path}";
            const string path = "/sookasa";
            const string token = "oesUCS8O0E2408OHISO0kdo08idoed9EO9bknoe08if9";
            const string contents = "stuff";

            var response = new RestResponse
                {
                    Content = String.Format(@"{{""contents"":""{0}""}}", contents)
                };

            _restProxy.Setup(x => x.Get(token, resource, It.IsAny<object>())).Returns(response);

            var actual = _sut.GetMetadata(token, path);

            Assert.That(actual, Is.EqualTo(contents));
        }

        [Test]
        public void ShouldPostTokenForCode()
        {
            const string resource = "/1/oauth2/token";
            const string code = "qRqFd2ecRK4AAAAAAAAAvY9a4V3r2L89mpyYlX0bkuE";
            const string token = "oesUCS8O0E2408OHISO0kdo08idoed9EO9bknoe08if9";

            var clientId = new KeyValuePair<string, string>("clientId", "75zoqoklvaz1chg");
            var clientSecret = new KeyValuePair<string, string>("clientSecret", "22zxi7sor5oycy3");
            var redirectUri = new KeyValuePair<string, string>("redirectUri", "http://localhost");
            var response = new RestResponse
                {
                    Content = String.Format(@"{{""access_token"":""{0}""}}", token)
                };

            _config.Setup(x => x.ClientId).Returns(clientId);
            _config.Setup(x => x.ClientSecret).Returns(clientSecret);
            _config.Setup(x => x.RedirectUri).Returns(redirectUri);

            var parameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { clientId.Key, clientId.Value },
                    { clientSecret.Key, clientSecret.Value },
                    { redirectUri.Key, redirectUri.Value }
                };

            _restProxy.Setup(x => x.Post(resource, null, parameters, null)).Returns(response);

            var actual = _sut.PostToken(code);

            Assert.That(actual, Is.EqualTo(token));

            _config.VerifyAll();
            _restProxy.VerifyAll();
        }
    }
}
