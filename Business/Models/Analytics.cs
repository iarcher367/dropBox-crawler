namespace Crawler.Business.Models
{
    using System;
    using System.Collections.Generic;

    public class Analytics
    {
        public int FileCount { get; set; }
        public int FolderCount { get; set; }

        public string LargestFilePath { get; set; }
        public Int64 LargestFileSize { get; set; }

        public string SmallestFilePath { get; set; }
        public Int64 SmallestFileSize { get; set; }

        public Dictionary<string, int> MimeTypes { get; set; }

        public Analytics()
        {
            MimeTypes = new Dictionary<string, int>();
        }
    }
}
