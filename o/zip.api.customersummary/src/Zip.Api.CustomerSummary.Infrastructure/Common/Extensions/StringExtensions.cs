using System.Collections.Generic;
using System.Linq;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceTags(this string str, Dictionary<string, string> dict)
        {
            if (dict != null)
            {
                // replace any supplied tokens in the SMS message
                return dict.Keys.Aggregate(str, (current, replacementTag) => current.Replace(replacementTag, dict[replacementTag]));
            }

            return str;
        }
    }
}
