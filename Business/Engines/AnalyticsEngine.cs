namespace Crawler.Business.Engines
{
    using DataAccess;
    using log4net;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Text;

    public class AnalyticsEngine : IAnalyticsEngine
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AnalyticsEngine));

        private readonly IDropBoxProxy _dropBoxProxy;

        public AnalyticsEngine(IDropBoxProxy dropBoxProxy)
        {
            _dropBoxProxy = dropBoxProxy;
        }

        public void AnalyzeFolder(string token, Analytics analytics, string path)
        {
            Log.DebugFormat("{0} processing folder {1}", token, path);

            var metadata = _dropBoxProxy.GetMetadata(token, path);

            foreach (var obj in JArray.Parse(metadata))
            {
                dynamic item = JsonConvert.DeserializeObject(obj.ToString());

                Log.DebugFormat("{0} processing {1}", token, item[Metadata.Path].Value);

                if (item[Metadata.IsDir].Value)
                {
                    analytics.FolderCount++;

                    AnalyzeFolder(token, analytics, item[Metadata.Path].Value);
                }
                else
                {
                    analytics.FileCount++;

                    Log.DebugFormat("{0} processing file #{1}", token, analytics.FileCount);

                    TrackFileSizeRange(analytics, item[Metadata.Path].Value, Convert.ToInt64(item[Metadata.Bytes].Value));

                    TrackMimeTypes(analytics, item[Metadata.MimeType].Value);
                }
            }
        }

        public string GenerateReport(Analytics analytics)
        {
            var occurrences = analytics.MimeTypes.ToList();

            occurrences.Sort((x, y) => -x.Value.CompareTo(y.Value));

            var stats = new StringBuilder();

            foreach (var pair in occurrences.Take(5))
                stats.Append(String.Format("{0} ({1})\n", pair.Key, pair.Value));

            var report = String.Format("File Count: {0} \nFolder Count: {1} \n" +
                "Biggest File: {2} ({3} bytes) \nSmallest File: {4} ({5} bytes) \n" +
                "Top 5 Mime Types: {6}",
                analytics.FileCount, analytics.FolderCount,
                analytics.LargestFilePath, analytics.LargestFileSize,
                analytics.SmallestFilePath, analytics.SmallestFileSize,
                stats);

            Log.InfoFormat("Account report\n{0}", report);

            return report;
        }

        private void TrackFileSizeRange(Analytics analytics, string filePath, Int64 fileSize)
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

                Log.DebugFormat("New smallest file found: {0} ({1} bytes)", filePath, fileSize);
            }
            else if (fileSize > analytics.LargestFileSize)
            {
                analytics.LargestFileSize = fileSize;
                analytics.LargestFilePath = filePath;

                Log.DebugFormat("New largest file found: {0} ({1} bytes)", filePath, fileSize);
            }
        }

        private void TrackMimeTypes(Analytics analytics, string mimeType)
        {
            if (analytics.MimeTypes.ContainsKey(mimeType))
            {
                analytics.MimeTypes[mimeType]++;

                Log.DebugFormat("Incrementing mime type {0} to {1}", mimeType, analytics.MimeTypes[mimeType]);
            }
            else
            {
                analytics.MimeTypes.Add(mimeType, 1);

                Log.DebugFormat("Adding new mime type: {0}", mimeType);
            }
        }
    }
}
