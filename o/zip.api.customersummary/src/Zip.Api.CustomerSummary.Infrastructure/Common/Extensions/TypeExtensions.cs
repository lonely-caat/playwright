using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class TypeExtensions
    {
        private const int MAX_TOPIC_NAME_LENGTH = 256;
        
        public static string ToTopicName(this Type type)
        {
            var name = type.IsGenericType
                ? Regex.Replace(type.FullName, "\\W", "_").ToLower()
                : type.Name.ToLower();

            name = name.Replace("command", string.Empty).Replace("event", string.Empty);

            if (name.Length > MAX_TOPIC_NAME_LENGTH)
            {
                var suffix = name.GetInvariantHashCode().ToString();
                name = name.Substring(0, MAX_TOPIC_NAME_LENGTH - suffix.Length) + suffix;
            }

            return name;
        }

        public static string ToCommandTopicName(this string typeName)
        {
            return Regex.Replace(typeName.ToLower(), "command", "");
        }

        public static string ToEventTopicName(this string typeName, string context)
        {
            return Regex.Replace(typeName.ToLower(), context, "").Replace("event", "");
        }

        public static string ToMessageTopicName(this string typeName)
        {
            return typeName.ToLower().Replace("message", "");
        }

        private static int GetInvariantHashCode(this string value)
        {
            return value.Aggregate(5381, (current, character) => (current * 397) ^ character);
        }
    }
}
