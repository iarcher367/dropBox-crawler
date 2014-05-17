namespace Crawler.Business
{
    using System;
    using Newtonsoft.Json.Linq;
    using Rest;
    using System.Collections.Generic;

    public sealed class DropBoxProxy : IDropBoxProxy
    {
        private readonly IConfig _config;
	    private readonly IRestProxy _restProxy;

        public DropBoxProxy(IRestProxy restProxy, IConfig config)
        {
            _config = config;
		    _restProxy = restProxy;
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

            return token.ToString();
        }

        // TODO: GetMetadata() - handle errors 304, 406
        public string GetMetadata(string path)
        {
            object obj = path;  // TODO: fix
            var response = _restProxy.Get("/1/metadata/{root}/{path}", obj);


            return null;
        }
    }
}
