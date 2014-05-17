namespace Crawler.Business
{
    using log4net;

    public class DropBoxManager : IDropBoxManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (DropBoxManager));

        private readonly IConfig _config;
        private readonly IOAuthEngine _oAuthEngine;
        private readonly IDropBoxProxy _dropBoxProxy;
	
	    public DropBoxManager(IDropBoxProxy dropBoxProxy, IOAuthEngine oAuthEngine, IConfig config)
	    {
	        _config = config;
	        _oAuthEngine = oAuthEngine;
	        _dropBoxProxy = dropBoxProxy;
	    }

        public string BuildAuthorizeUrl()
        {
            var url = _oAuthEngine.GetAuthorizeUrl();

            Log.DebugFormat("Generated OAuth 2.0 code flow url: {0}", url);

            return url;
        }

        public string AcquireBearerToken(string code)
        {
            var token = _dropBoxProxy.PostToken(code);

            Log.InfoFormat("Converted code {0} to bearer token {1}", code, token);

            return token;
        }

        public string Crawl(string email)
        {
            return null;
        }
    }
}
