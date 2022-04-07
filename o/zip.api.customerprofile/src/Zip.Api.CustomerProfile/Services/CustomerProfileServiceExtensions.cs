using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.Models;

namespace Zip.Api.CustomerProfile.Services
{
    public static class CustomerProfileServiceExtensions
    {
        public static async Task<IEnumerable<Customer>> GetCustomersByIdAsync(
            this ICustomerProfileService customerProfileService,
            string customerId,
            string correlationId)
        {
            var customerResponseOption = new CustomerResponseOptions
            {
                IsIncludedSocialMediaS3 = true
            };
            var results = await customerProfileService.GetCustomersById(new[] {Guid.Parse(customerId)},
                correlationId, customerResponseOption);
            return results;
        }
    }
}