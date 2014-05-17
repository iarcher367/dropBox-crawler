namespace Crawler.Business
{
    using System.Collections.Generic;

    public interface IConfig
    {
        KeyValuePair<string, string> Authorization { get; }
        string BaseUrl { get; }
        string BearerToken { get; set; }
        KeyValuePair<string, string> ClientId { get; }
        KeyValuePair<string, string> ClientSecret { get; }
        string OAuth2Endpoint { get; }
        KeyValuePair<string, string> RedirectUri { get; }
    }
}
