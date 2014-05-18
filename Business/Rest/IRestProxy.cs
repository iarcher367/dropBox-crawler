namespace Crawler.Business.Rest
{
    using RestSharp;
    using System.Collections.Generic;

    public interface IRestProxy
    {
        IRestResponse Get(string token, string resource, object urlSegments);
        IRestResponse Post(string resource, object urlSegments = null, IEnumerable<KeyValuePair<string, string>> parameters = null, object body = null);
    }
}
