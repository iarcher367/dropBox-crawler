namespace Business.Rest
{
    using RestSharp;

    public class RestProxy : IRestProxy
    {
        private readonly IRestClient _restClient;

        public RestProxy(IRestClient restClient, IConfig config)
        {
            _restClient = restClient;
            _restClient.BaseUrl = config.BaseUrl;
        }

        public IRestResponse Get(string resource, object urlSegments)
        {
            var request = new RestRequest(resource, Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegments(urlSegments);

            return _restClient.Execute(request);
        }

        public IRestResponse Post(string resource, object urlSegments, object body)
        {
            var request = new RestRequest(resource, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddUrlSegments(urlSegments).AddBody(body);

            return _restClient.Execute(request);
        }
    }
}
