namespace Crawler.Business.Engines
{
    using System;
    using Models;

    public interface IAnalyticsEngine
    {
        string GenerateReport(Analytics analytics);
        void TrackFileSizeRange(Analytics analytics, string filePath, Int64 fileSize);
        void TrackMimeTypes(Analytics analytics, string mimeType);
    }
}
