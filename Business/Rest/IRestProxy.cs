namespace Crawler.Business.Rest
{
    using System.Collections.Generic;
    using RestSharp;

    public interface IRestProxy
    {
        IRestResponse Get(string resource, object urlSegments);
        IRestResponse Post(string resource, object urlSegments = null, IEnumerable<KeyValuePair<string, string>> parameters = null, object body = null);
    }
}
