using System.Collections.Generic;
using Zip.Api.CustomerProfile.GraphQL;
using Zip.Core.Serilog;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.ElasticSearch.Entities;

namespace Zip.Api.CustomerProfile.Helpers
{
    public static class SerilogHelper
    {
        public static object LogSensitiveCustomerInfo(Customer customer)
        {
            var dict = new Dictionary<string, string>
            {
                ["email"] = customer?.Email,
                ["phone"] = customer?.MobilePhone?.PhoneNumber,
                ["medicareId"] = customer?.Medicare?.Id,
                ["driverLicenseId"] = customer?.DriverLicence?.Id,
                [CustomerQuery.ArgumentComparableAddress] = customer?.ResidentialAddress?.ComparableAddress,
                [CustomerQuery.ArgumentVedaReference] = customer?.VedaReferenceNumber
            };
            var result = new
            {
                customerId = customer?.Id,
                info = LogHelper.HashItems(dict)
            };
            return result;
        }

        public static object LogSensitiveCustomerInfo(CustomerEntity customer)
        {
            var dict = new Dictionary<string, string>
            {
                ["email"] = customer?.Email,
                ["phone"] = customer?.Mobile,
                ["medicareId"] = customer?.Medicare?.Id,
                ["driverLicenseId"] = customer?.DriverLicence?.Id,
                [CustomerQuery.ArgumentComparableAddress] = customer?.ResidentialComparableAddress,
                [CustomerQuery.ArgumentVedaReference] = customer?.VedaReferenceNumber
            };
            var result = new
            {
                customerId = customer?.Id,
                info = LogHelper.HashItems(dict)
            };
            return result;
        }
    }
}