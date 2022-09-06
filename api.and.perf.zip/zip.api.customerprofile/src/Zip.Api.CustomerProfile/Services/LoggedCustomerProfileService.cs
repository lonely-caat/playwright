using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.Core.Extensions;
using Zip.Core.Serilog;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.Models;
using CustomerQuery = Zip.Api.CustomerProfile.GraphQL.CustomerQuery;

namespace Zip.Api.CustomerProfile.Services
{
    public class LoggedCustomerProfileService : ICustomerProfileService
    {
        private const string ArgumentComparableAddress = "residentialcomparableaddress";
        private const string ArgumentVedaReference = "vedaReferenceNumber";
        private readonly ICustomerProfileService _customerProfileServiceImplementation;
        private readonly Type _customerProfileServiceType;
        private readonly ILogger _logger;

        public LoggedCustomerProfileService(ICustomerProfileService customerProfileServiceImplementation,
            ILogger logger)
        {
            _customerProfileServiceImplementation = customerProfileServiceImplementation ??
                                                    throw new ArgumentNullException(
                                                        nameof(customerProfileServiceImplementation));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _customerProfileServiceType = _customerProfileServiceImplementation.GetType();
        }

        public Task<IEnumerable<Customer>> GetCustomersById(IEnumerable<Guid> ids, string correlationId,
            CustomerResponseOptions customerResponseOption)
        {
            if (ids == null) throw new ArgumentNullException(nameof(ids));

            if (correlationId == null)
                throw new ArgumentNullException(nameof(correlationId));

            var customerIds = string.Join(",", ids.Select(id => id.ToString()));
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{GetType().FullName}.{methodName} GraphQL query by customer Id: {SerilogProperty.CustomerId}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                customerIds);
            return _customerProfileServiceImplementation.GetCustomersById(ids, correlationId, customerResponseOption);
        }

        public Task<IEnumerable<Customer>> GetCustomersByPaypalId(string paypalId, string correlationId,
            params string[] fields)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileServiceType.FullName}.{methodName} GraphQL query by paypal Id: {SerilogProperty.Detail}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                paypalId);
            return _customerProfileServiceImplementation.GetCustomersByPaypalId(paypalId, correlationId, fields);
        }

        public Task<IEnumerable<Customer>> GetCustomersByFacebookId(string facebookId, string correlationId,
            params string[] fields)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileServiceType.FullName}.{methodName}:: GraphQL query by facebook Id: {SerilogProperty.Detail}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                facebookId);
            return _customerProfileServiceImplementation.GetCustomersByFacebookId(facebookId, correlationId, fields);
        }

        public Task<IEnumerable<Customer>> GetCustomersByMobile(string mobile, string correlationId,
            params string[] fields)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileServiceType.FullName}.{methodName}:: GraphQL query by mobile: {SerilogProperty.Detail}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                mobile.GetHashSHA256());
            return _customerProfileServiceImplementation.GetCustomersByMobile(mobile, correlationId, fields);
        }

        public Task<IEnumerable<Customer>> GetCustomersByKeyword(string keyword, string correlationId,
            params string[] fields)
        {
            return _customerProfileServiceImplementation.GetCustomersByKeyword(keyword, correlationId, fields);
        }

        public Task<IEnumerable<Customer>> GetCustomersByComparableAddress(string residentialComparableAddress,
            string correlationId, params string[] fields)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileServiceType.FullName}.{methodName}:: GraphQL query by {ArgumentComparableAddress}: {SerilogProperty.Detail}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                residentialComparableAddress.GetHashSHA256());
            return _customerProfileServiceImplementation.GetCustomersByComparableAddress(residentialComparableAddress,
                correlationId, fields);
        }

        public Task<IEnumerable<Customer>> GetCustomersByVedaReferenceNumber(string vedaReferenceNumber,
            string correlationId, params string[] fields)
        {
            var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
            _logger.LogInformation(
                $"{_customerProfileServiceType.FullName}.{methodName} GraphQL query by {ArgumentVedaReference}: {SerilogProperty.Detail}.",
                nameof(CustomerQuery),
                nameof(CustomerQuery),
                vedaReferenceNumber.GetHashSHA256());
            return _customerProfileServiceImplementation.GetCustomersByVedaReferenceNumber(vedaReferenceNumber,
                correlationId, fields);
        }
    }
}