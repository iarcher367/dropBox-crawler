namespace Crawler.Business
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public class Config : IConfig
    {
        public KeyValuePair<string, string> Authorization
        {
            get { return new KeyValuePair<string, string>("Authorization", String.Format("Bearer {0}", BearerToken)); }
        }

        public string BaseUrl { get { return ConfigurationManager.AppSettings["BaseUrl"]; } }

        public string BearerToken { get; set; }

        public KeyValuePair<string, string> ClientId
        {
            get { return new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["ClientId"]); }
        }

        public KeyValuePair<string, string> ClientSecret
        {
            get { return new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["ClientSecret"]); }
        }

        public string OAuth2Endpoint { get { return ConfigurationManager.AppSettings["OAuth2Endpoint"]; } }

        public KeyValuePair<string, string> RedirectUri
        {
            get { return new KeyValuePair<string, string>("redirect_uri", ConfigurationManager.AppSettings["RedirectUri"]); }
        }
    }
}
