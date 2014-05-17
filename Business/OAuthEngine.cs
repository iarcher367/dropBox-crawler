namespace Crawler.Business
{
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class OAuthEngine : IOAuthEngine
    {
        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public OAuthEngine(IRestClient restClient, IConfig config)
        {
            _config = config;
            _restClient = restClient;
        }

        public string GetAuthorizeUrl()
        {
            _restClient.BaseUrl = _config.OAuth2Endpoint;

            var request = new RestRequest(String.Empty, Method.GET);

            var parameters = new Dictionary<string, string>
                {
                    { "response_type", "code" },
                    { _config.ClientId.Key, _config.ClientId.Value },
                    { _config.RedirectUri.Key, _config.RedirectUri.Value }
                };

            foreach (var param in parameters)
                request.AddParameter(param.Key, param.Value);

            return _restClient.BuildUri(request).ToString();
        }
    }
}
