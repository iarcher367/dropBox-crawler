namespace Crawler.Business.Rest
{
    using System;

    [Serializable]
    public class RestException : Exception
    {
        private const string _message = "Error processing request. Check inner details for more info.";

        public RestException(Exception innerException) : base(_message, innerException)
        {
        }
    }
}
