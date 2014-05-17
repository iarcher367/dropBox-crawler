namespace Crawler.Business
{
    public class DropBoxManager : IDropBoxManager
    {
        private readonly IConfig _config;
        private readonly IOAuthEngine _oAuthEngine;
        private readonly IDropBoxProxy _proxy;
	
	    public DropBoxManager(IDropBoxProxy proxy, IOAuthEngine oAuthEngine, IConfig config)
	    {
	        _config = config;
	        _oAuthEngine = oAuthEngine;
	        _proxy = proxy;
	    }

        public string GetAuthorizeUrl()
        {
            return _oAuthEngine.GetAuthorizeUrl();
        }

        public string Crawl(string[] emails)
        {
            return null;
        }
    }
}
