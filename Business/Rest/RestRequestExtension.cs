namespace Crawler.Business.Rest
{
    using RestSharp;
    using System;
    using System.Linq;

    internal static class RestRequestExtension
    {
        public static IRestRequest AddUrlSegments(this IRestRequest request, object parameters)
        {
            var properties = parameters.GetType().GetProperties();

            var pairs = properties.Where(x => IsAllowedType(x.PropertyType))
                .Select(x => new {x.Name, Value = x.GetValue(parameters, null)});
            // TODO: check logic
            pairs.Aggregate(request, (x, y) => x.AddUrlSegment(y.Name, y.Value.ToString()));

            return request;
        }

        private static bool IsAllowedType(Type type)
        {
            return type.IsValueType || type == typeof (string);
        }
    }
}
