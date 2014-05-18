namespace Crawler.Business.DataAccess
{
    public interface IDropBoxProxy
    {
        string GetMetadata(string token, string path);
        string PostToken(string code);
    }
}
