using System.Collections.Generic;
using System.Linq;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class CommonExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !IsNotEmpty(collection);
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }
    }
}
