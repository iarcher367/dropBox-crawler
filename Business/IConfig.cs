namespace Business
{
    using System.Collections.Generic;

    public interface IConfig
    {
        string ApiKey { get; }
        string ApiSecret { get; }
        KeyValuePair<string, string> AuthHeader { get; }
        string BaseUrl { get; }
        string BearerToken { get; set; }
    }
}
