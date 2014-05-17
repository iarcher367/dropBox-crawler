namespace Crawler.Business.Engines
{
    using Models;
    using System;
    using System.Linq;
    using System.Text;

    public class AnalyticsEngine : IAnalyticsEngine
    {
        public string GenerateReport(Analytics analytics)
        {
            var occurrences = analytics.MimeTypes.ToList();

            occurrences.Sort((x, y) => -x.Value.CompareTo(y.Value));

            var stats = new StringBuilder();

            foreach (var pair in occurrences.Take(5))
                stats.Append(String.Format("{0} ({1})\n", pair.Key, pair.Value));

            return String.Format("File Count: {0} \nFolder Count: {1} \n" +
                "Biggest File: {2} ({3} bytes) \nSmallest File: {4} ({5} bytes) \n" +
                "Top 5 Mime Types: {6}",
                analytics.FileCount, analytics.FolderCount,
                analytics.LargestFilePath, analytics.LargestFileSize,
                analytics.SmallestFilePath, analytics.SmallestFileSize,
                stats);
        }

        public void TrackFileSizeRange(Analytics analytics, string filePath, Int64 fileSize)
        {
            if (analytics.SmallestFileSize == 0 && String.IsNullOrEmpty(analytics.SmallestFilePath))
            {
                analytics.SmallestFileSize = fileSize;
                analytics.SmallestFilePath = filePath;
            }
            
            if (fileSize < analytics.SmallestFileSize)
            {
                analytics.SmallestFileSize = fileSize;
                analytics.SmallestFilePath = filePath;
            }
            else if (fileSize > analytics.LargestFileSize)
            {
                analytics.LargestFileSize = fileSize;
                analytics.LargestFilePath = filePath;
            }
        }

        public void TrackMimeTypes(Analytics analytics, string mimeType)
        {
            if (analytics.MimeTypes.ContainsKey(mimeType))
                analytics.MimeTypes[mimeType]++;
            else
                analytics.MimeTypes.Add(mimeType, 1);
        }
    }
}
