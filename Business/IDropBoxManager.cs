namespace Crawler.Business
{
    public interface IDropBoxManager
    {
        string BuildAuthorizeUrl();
        string AcquireBearerToken(string code);
        string Crawl(string email);
    }
}
