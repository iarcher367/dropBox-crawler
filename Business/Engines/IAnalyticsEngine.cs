namespace Crawler.Business.Engines
{
    using Models;

    public interface IAnalyticsEngine
    {
        void AnalyzeFolder(string token, Analytics analytics, string path);
        string GenerateReport(Analytics analytics);
    }
}
