namespace Crawler.Business.Engines
{
    using log4net;
    using RestSharp;
    using System;
    using System.Collections.Generic;

    public class OAuthEngine : IOAuthEngine
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(OAuthEngine));

        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public OAuthEngine(IRestClient restClient, IConfig config)
        {
            _config = config;
            _restClient = restClient;
        }

        public string BuildAuthorizeUrl()
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

            var uri = _restClient.BuildUri(request).ToString();
            
            Log.InfoFormat("Generated OAuth 2.0 code flow url: {0}", uri);

            return uri;
        }
    }
}
