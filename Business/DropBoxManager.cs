namespace Crawler.Business
{
    using Engines;
    using log4net;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
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

        public string Crawl(string token)
        {
            var analytics = new Analytics();

            Log.InfoFormat("Crawling account for token {0}", token);

            AnalyzeFolder(token, analytics, String.Empty);

            return _analyticsEngine.GenerateReport(analytics);
        }

        private void AnalyzeFolder(string token, Analytics analytics, string path)
        {
            var metadata = _dropBoxProxy.GetMetadata(token, path);

            foreach (var obj in JArray.Parse(metadata))
            {
                dynamic item = JsonConvert.DeserializeObject(obj.ToString());

                Log.DebugFormat("{0} processing {1}", token, item["path"].Value);

                if (item[Metadata.IsDir].Value)
                {
                    analytics.FolderCount++;

                    AnalyzeFolder(token, analytics, item[Metadata.Path].Value);
                }
                else
                {
                    analytics.FileCount++;

                    _analyticsEngine.TrackFileSizeRange(analytics, item[Metadata.Path].Value, Convert.ToInt64(item[Metadata.Bytes].Value));

                    _analyticsEngine.TrackMimeTypes(analytics, item[Metadata.MimeType].Value);
                }
            }
        }
    }
}
