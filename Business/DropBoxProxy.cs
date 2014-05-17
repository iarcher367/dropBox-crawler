namespace Crawler.Business
{
    using Rest;

    public sealed class DropBoxProxy : IDropBoxProxy
    {
	    private readonly IRestProxy _restProxy;

        public DropBoxProxy(IRestProxy restProxy)
	    {
		    _restProxy = restProxy;
	    }

        // TODO: GetMetadata() - handle errors 304, 406
        public string GetMetadata(string path)
        {
            object obj = path;  // TODO: fix
            var response = _restProxy.Get("/1/metadata/{root}/{path}", obj);


            return null;
        }
    }
}
