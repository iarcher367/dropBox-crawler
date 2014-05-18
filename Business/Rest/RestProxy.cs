namespace Crawler.Business.Rest
{
    using log4net;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RestProxy : IRestProxy
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RestProxy));

        private readonly IConfig _config;
        private readonly IRestClient _restClient;

        public RestProxy(IRestClient restClient, IConfig config)
        {
            _config = config;
            _restClient = restClient;
            _restClient.BaseUrl = config.BaseUrl;
        }

        public IRestResponse Get(string token, string resource, object urlSegments)
        {
            var request = new RestRequest(resource, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegments(urlSegments);

            if (!String.IsNullOrEmpty(token))
                request.AddHeader(_config.Authorization.Key, String.Format(_config.Authorization.Value, token));

            Log.DebugFormat("Generated GET to {0}", _restClient.BuildUri(request));

            return _restClient.Execute(request);
        }

        public IRestResponse Post(string resource, object urlSegments = null, IEnumerable<KeyValuePair<string, string>> parameters = null, object body = null)
        {
            var request = new RestRequest(resource, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegments(urlSegments)
                .AddParameters(parameters)
                .AddBody(body);

            Log.DebugFormat("Generated POST to {0} with parameters:\n{1}",
                _restClient.BuildUri(request),
                request.Parameters
                    .FindAll(x => x.Type == ParameterType.GetOrPost)
                    .Select(x => String.Format("{0}: {1}", x.Name, x.Value))
                    .Aggregate((m, n) => m + "\n" + n)
                );

            return _restClient.Execute(request);
        }
    }
}
