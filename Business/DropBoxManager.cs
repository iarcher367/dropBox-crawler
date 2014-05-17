namespace Crawler.Business
{
    public class DropBoxManager : IDropBoxManager
    {
	    private readonly IDropBoxProxy _proxy;
	
	    public DropBoxManager(IDropBoxProxy proxy)
	    {
	        _proxy = proxy;
	    }

        public void Authenticate()
        {
            // response.Content
            // response.ErrorException
        }

        public string Crawl(string[] emails)
        {
            return null;
        }
    }
}
