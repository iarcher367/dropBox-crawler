﻿namespace Crawler.Business
{
    public interface IDropBoxManager
    {
        string GetAuthorizeUrl();
        string Crawl(string[] emails);
    }
}
