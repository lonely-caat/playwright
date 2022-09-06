using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.Models;

namespace Zip.Api.CustomerProfile.Interfaces
{
    public interface ICustomerProfileService
    {
        Task<IEnumerable<Customer>> GetCustomersById(IEnumerable<Guid> ids, string correlationId,
            CustomerResponseOptions customerResponseOption);

        Task<IEnumerable<Customer>> GetCustomersByPaypalId(string paypalId, string correlationId,
            params string[] fields);

        Task<IEnumerable<Customer>> GetCustomersByFacebookId(string facebookId, string correlationId,
            params string[] fields);

        Task<IEnumerable<Customer>> GetCustomersByMobile(string mobile, string correlationId, params string[] fields);

        Task<IEnumerable<Customer>> GetCustomersByKeyword(string keyword, string correlationId, params string[] fields);

        Task<IEnumerable<Customer>> GetCustomersByComparableAddress(string residentialComparableAddress,
            string correlationId, params string[] fields);

        Task<IEnumerable<Customer>> GetCustomersByVedaReferenceNumber(string vedaReferenceNumber, string correlationId,
            params string[] fields);
    }
}