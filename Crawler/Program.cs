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
                var url = manager.BuildAuthorizeUrl();

                foreach (var email in args)
                {
                    Log.InfoFormat("{0} : Initializing crawler", email);

                    Process.Start(url);

                    var code = PromptForOAuthCode(email);
                    var token = manager.AcquireBearerToken(code);
                }
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

            Log.DebugFormat("{0} : Received OAuth code flow code {1}", email, code);

            return code;
        }
    }
}
