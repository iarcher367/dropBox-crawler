namespace Crawler.Business
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class Config : IConfig
    {
        public string ApiKey { get { return ConfigurationManager.AppSettings["ApiKey"]; } }

        public string ApiSecret { get { return ConfigurationManager.AppSettings["ApiSecret"]; } }

        public KeyValuePair<string, string> AuthHeader
        {
            get
            {
                return new KeyValuePair<string, string>("Authorization",
                    String.Format("Bearer {0}", BearerToken));
            }
        }

        public string BaseUrl { get { return ConfigurationManager.AppSettings["BaseUrl"]; } }

        public string BearerToken { get; set; }
    }
}
