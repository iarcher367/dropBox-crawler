namespace Business
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
            
        }

        public string Crawl(string[] emails)
        {
            return null;
        }
    }
}
