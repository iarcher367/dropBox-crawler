namespace Crawler.Test.Engines
{
    using Business;
    using Business.Engines;
    using Moq;
    using NUnit.Framework;
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class OAuthEngineTest
    {
        private IOAuthEngine _sut;
        private Mock<IConfig> _config;
        private Mock<IRestClient> _restClient;
        
        [SetUp]
        public void Setup()
        {
            _config = new Mock<IConfig>();
            _restClient = new Mock<IRestClient>();
            _sut = new OAuthEngine(_restClient.Object, _config.Object);
        }

        [Test]
        public void ShouldReturnAuthorizeUrl()
        {
            const string url = "https://www.dropbox.com/1/oauth2/authorize?response_type=code" +
                "&client_id=75zoqoklvaz1chg&redirect_uri=http://localhost";
            const string oAuth2Endpoint = "http://www.google.com";
            var clientId = new KeyValuePair<string, string>("clientId", "75zoqoklvaz1chg");
            var redirectUri = new KeyValuePair<string, string>("redirectUri", "http://localhost");

            _config.Setup(x => x.OAuth2Endpoint).Returns(oAuth2Endpoint);
            _config.Setup(x => x.ClientId).Returns(clientId);
            _config.Setup(x => x.RedirectUri).Returns(redirectUri);

            _restClient.Setup(x => x.BuildUri(It.IsAny<RestRequest>()))
                .Returns(new Uri(url));

            var actual = _sut.BuildAuthorizeUrl();

            Assert.That(actual, Is.EqualTo(url));

            _config.VerifyAll();
            _restClient.VerifyAll();
        }
    }
}
