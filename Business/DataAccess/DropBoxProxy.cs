namespace Crawler.Business.DataAccess
{
    using log4net;
    using Newtonsoft.Json.Linq;
    using Rest;
    using System;
    using System.Collections.Generic;

    public sealed class DropBoxProxy : IDropBoxProxy
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DropBoxProxy));

        private readonly IConfig _config;
	    private readonly IRestProxy _restProxy;

        public DropBoxProxy(IRestProxy restProxy, IConfig config)
        {
            _config = config;
		    _restProxy = restProxy;
	    }

        public string GetMetadata(string token, string path)
        {
            const string resource = "/1/metadata/{root}/{path}";

            var urlSegments = new
            {
                root = "dropbox",
                path
            };

            var response = _restProxy.Get(token, resource, urlSegments);
            var contents = JObject.Parse(response.Content).SelectToken("contents").ToString();

            Log.DebugFormat("Retrieved account contents: {0}", contents);

            return contents;
        }

        public string PostToken(string code)
        {
            const string resource = "/1/oauth2/token";

            var parameters = new Dictionary<string, string>
                {
                    { "code", code },
                    { "grant_type", "authorization_code" },
                    { _config.ClientId.Key, _config.ClientId.Value },
                    { _config.ClientSecret.Key, _config.ClientSecret.Value },
                    { _config.RedirectUri.Key, _config.RedirectUri.Value }
                };

            var response = _restProxy.Post(resource, parameters: parameters);
            var token = JObject.Parse(response.Content).SelectToken("access_token");

            if (token == null)
                throw new Exception("Unable to acquire bearer token. Probable cause: code has expired");

            Log.DebugFormat("Retrieved token: {0}", token);

            return token.ToString();
        }
    }
}
