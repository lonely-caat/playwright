using System;
using System.Text.RegularExpressions;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Utilities
{
    public static class PhoneNumberHelper
    {
        private const string AustraliaCountryCode = "+61";
        private const string NzCountryCode = "+64";

        public static string ToMobileLocalFormat(string s)
        {
            // remove all but digits and +. 
            s = Regex.Replace(s, @"[^\d+]", string.Empty);

            if (Regex.IsMatch(s, @"^\+6(14|15|42)"))
            {
                s = "0" + s.Remove(0, 3);
                return s;
            }

            if (Regex.IsMatch(s, @"^(4|5|2)\d+"))
            {
                s = "0" + s;
                return s;
            }

            return s;
        }

        public static string ToMobileIntFormat(string s)
        {
            var orig = s;

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            // remove all but digits and +. 
            s = Regex.Replace(s, @"[^\d+]", string.Empty);

            if (s.StartsWith("+"))
            {
                return s;
            }

            if (s.StartsWith("04") || s.StartsWith("05"))
            {
                // au
                s = s.Remove(0, 1).Insert(0, AustraliaCountryCode);
                return s;
            }

            if (s.StartsWith("02"))
            {
                s = s.Remove(0, 1).Insert(0, NzCountryCode);
                return s;
            }

            throw new FormatException($"Invalid Mobile Phone number {orig} is not a supported phone number.");
        }
    }
}
