namespace Crawler.Business
{
    public interface IDropBoxManager
    {
        string AcquireBearerToken(string code);
        string BuildAuthorizeUrl();
        string Crawl(string token);
    }
}
