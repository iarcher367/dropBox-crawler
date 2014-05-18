namespace Crawler.Console
{
    using Business;
    using Business.DataAccess;
    using Business.Engines;
    using Business.Rest;
    using RestSharp;
    using SimpleInjector;

    internal static class Global
    {
        public static Container Container { get; private set; }

        static Global()
        {
            Container = new Container();

            Container.RegisterSingle<IConfig, Config>();
            Container.Register<IAnalyticsEngine, AnalyticsEngine>();
            Container.Register<IDropBoxManager, DropBoxManager>();
            Container.Register<IDropBoxProxy, DropBoxProxy>();
            Container.Register<IOAuthEngine, OAuthEngine>();
            Container.Register<IRestClient, Business.Rest.RestClient>();
            Container.Register<IRestProxy, RestProxy>();

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
