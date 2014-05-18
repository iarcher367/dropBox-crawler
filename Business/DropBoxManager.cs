namespace Crawler.Business
{
    using DataAccess;
    using Engines;
    using log4net;
    using Models;
    using System;

    public class DropBoxManager : IDropBoxManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (DropBoxManager));

        private readonly IAnalyticsEngine _analyticsEngine;
        private readonly IOAuthEngine _oAuthEngine;
        private readonly IDropBoxProxy _dropBoxProxy;
	
	    public DropBoxManager(IDropBoxProxy dropBoxProxy, IOAuthEngine oAuthEngine, IAnalyticsEngine analyticsEngine)
	    {
	        _analyticsEngine = analyticsEngine;
	        _oAuthEngine = oAuthEngine;
	        _dropBoxProxy = dropBoxProxy;
	    }

        public string BuildAuthorizeUrl()
        {
            Log.Info("Building Authorize Url...");

            return _oAuthEngine.BuildAuthorizeUrl();
        }

        public string AcquireBearerToken(string code)
        {
            Log.InfoFormat("Acquiring bearer token for {0}...", code);

            var token = _dropBoxProxy.PostToken(code);

            Log.InfoFormat("Converted code {0} to bearer token {1}", code, token);

            return token;
        }

        public string Crawl(string token)
        {
            Log.InfoFormat("Crawling account for token {0}...", token);

            var analytics = new Analytics();

            _analyticsEngine.AnalyzeFolder(token, analytics, String.Empty);

            Log.InfoFormat("Generating report for token {0}...", token);

            return _analyticsEngine.GenerateReport(analytics);
        }
    }
}
