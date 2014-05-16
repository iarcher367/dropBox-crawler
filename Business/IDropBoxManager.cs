namespace Business
{
    public interface IDropBoxManager
    {
        void Authenticate();
        string Crawl(string[] emails);
    }
}
