namespace Crawler.Business.Rest
{
    using RestSharp;
    using System;
    using System.Linq;

    public static class RestRequestExtension
    {
        public static IRestRequest AddUrlSegments(this IRestRequest request, object parameters)
        {
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
    }
}
