namespace Crawler.Business.Rest
{
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RestRequestExtension
    {
        public static IRestRequest AddUrlSegments(this IRestRequest request, object parameters)
        {
            if (parameters == null) return request;

            var pairs = parameters.GetType().GetProperties()
                .Where(x => IsAllowedType(x.PropertyType))
                .Select(x => new {x.Name, Value = x.GetValue(parameters, null)});

            foreach (var pair in pairs)
                request.AddUrlSegment(pair.Name, pair.Value.ToString());

            return request;
        }

        private static bool IsAllowedType(Type type)
        {
            return type.IsValueType || type == typeof (string);
        }

        public static IRestRequest AddParameters(this IRestRequest request, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (parameters == null) return request;

            foreach (var pair in parameters)
                request.AddParameter(pair.Key, pair.Value);

            return request;
        }
    }
}
