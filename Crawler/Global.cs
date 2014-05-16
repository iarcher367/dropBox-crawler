namespace DropBoxCrawler
{
    using Business;
    using Business.Rest;
    using SimpleInjector;

    internal static class Global
    {
        public static Container Container { get; private set; }

        static Global()
        {
            Container = new Container();

            Container.RegisterSingle<IConfig, Config>();
            Container.Register<IDropBoxManager, DropBoxManager>();
            Container.Register<IDropBoxProxy, DropBoxProxy>();
            Container.Register<IRestProxy, RestProxy>();
        }
    }
}
