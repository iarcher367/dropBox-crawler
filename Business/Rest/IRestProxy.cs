namespace Crawler.Business.Rest
{
    using RestSharp;

    public interface IRestProxy
    {
        IRestResponse Get(string resource, object urlSegments);
        IRestResponse Post(string resource, object urlSegments, object body);
    }
}
