namespace Crawler.Console
{
    using Business;
    using log4net;
    using System;
    using System.Diagnostics;

    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Program));

        public static void Main(string[] args)
        {
            try
            {
                var manager = Global.Container.GetInstance<IDropBoxManager>();
                var url = manager.GetAuthorizeUrl();
                Process.Start(url);

                var data = manager.Crawl(args);

                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }


    }
}
