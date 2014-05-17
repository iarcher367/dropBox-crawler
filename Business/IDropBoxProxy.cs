namespace Crawler.Business
{
    public interface IDropBoxProxy
    {
        string GetMetadata(string token, string path);
        string PostToken(string code);
    }
}
