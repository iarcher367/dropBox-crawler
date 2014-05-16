namespace DropBoxCrawler
{
    using Business;
    using log4net;
    using System;

    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Program));

        public static void Main(string[] args)
        {
            try
            {
                var manager = Global.Container.GetInstance<IDropBoxManager>();
                manager.Authenticate();

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
