namespace Crawler.Console
{
    using Business;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    internal class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Program));

        public static void Main(string[] args)
        {
            try
            {
                var accounts = new Dictionary<string, string>();
                var manager = Global.Container.GetInstance<IDropBoxManager>();
                var url = manager.BuildAuthorizeUrl();

                foreach (var email in args)
                {
                    Log.InfoFormat("Initializing crawler for {0}", email);

                    Process.Start(url);

                    var code = PromptForOAuthCode(email);

                    var token = manager.AcquireBearerToken(code);

                    accounts.Add(email, token);
                }

                Parallel.ForEach(accounts, x =>
                    Console.WriteLine("Processing completed for {0}. Summary: \n{1}",
                        x.Key,
                        manager.Crawl(x.Value))
                );

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        private static string PromptForOAuthCode(string email)
        {
            Console.Write("OAuth code for {0}? ", email);
            var code = Console.ReadLine();

            Log.InfoFormat("{0} : Received OAuth code flow code {1}", email, code);

            return code;
        }
    }
}
