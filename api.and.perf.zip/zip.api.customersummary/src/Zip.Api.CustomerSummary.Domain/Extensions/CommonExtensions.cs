using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace Zip.Api.CustomerSummary.Domain.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CommonExtensions
    {
        private static readonly Regex _regex = new Regex("\\w+\\.cs:line\\s\\d+");

        public static string GetErrorLinesInfo(this Exception exception)
        {
            var matches = _regex.Matches(exception.StackTrace ?? string.Empty);
            if (matches.Any())
            {
                return string.Join(", ", matches.Select(x => x.Value));
            }

            return string.Empty;
        }
    }
}
