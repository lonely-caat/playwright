using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Zip.Api.CustomerSummary.Domain.Entities.ApplicationEvent;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService
{
    public class NamingStrategy : INamingStrategy
    {
        private static readonly Dictionary<string, string> _namingEventContexts = new Dictionary<string, string>
        {
            // deprecate
            {nameof(CustomerDetailsUpdatedEvent), "Zip-{0}-customer-detailsupdated"},
            {nameof(AccountDetailsUpdatedEvent), "Zip-{0}-account-detailsupdated"},
            {nameof(ApplicationDetailsUpdatedEvent), "Zip-{0}-application-detailsupdated"},
            {nameof(AccountsBillingPeriodClosedCommand),  "Zip-{0}-accountstatement-accountsbillingperiodclosed"},
        };

        private static string EvnName
        {
            get
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                switch (env)
                {
                    case "dev": return "development";
                    case "sand": return "sandbox";
                    case "prod": return "production";
                    default: return env;
                }
            }
        }

        protected string CleanName(string name)
        {
            return Regex.Replace(name.ToLower(), "message", string.Empty);
        }

        public string GetTopicName(Type messageType)
        {
            if (_namingEventContexts.ContainsKey(messageType.Name))
            {
                return string.Format(_namingEventContexts[messageType.Name], EvnName);
            }

            throw new KeyNotFoundException($"Can not find the context for messageType : {messageType} ");
        }
    }
}
