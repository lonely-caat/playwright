using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Prometheus;
using Zip.Api.CustomerProfile.Helpers;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.Core.Extensions;
using Zip.Core.Prometheus;
using Zip.Core.Serilog;
using Zip.CustomerProfile.Contracts;
using Zip.CustomerProfile.Data.ElasticSearch.Entities;
using Zip.CustomerProfile.Data.ElasticSearch.Models;
using Zip.CustomerProfile.Data.Interfaces;
using DataModels = Zip.CustomerProfile.Data.Models;

namespace Zip.Api.CustomerProfile.Services
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private const string S3SocialMediaRequest = "socialMediaData";
        private readonly Counter _counter;
        private readonly IDataProvider _dataAccessProvider;
        private readonly IElasticSearchDataProvider _esCustomerProviderService;

        private readonly string[] _fieldsReadableFormEs =
        {
            "id", "givenName", "familyName", "otherName", "email", "driverLicence", "medicare",
            "residentialComparableAddress", "vedaReferenceNumber"
        };

        private readonly Histogram _histogram;
        private readonly ILogger<CustomerProfileService> _logger;
        private readonly IMapper _mapper;

        public CustomerProfileService(IElasticSearchDataProvider esCustomerProviderService,
            IDataProvider dataAccessProvider,
            IMapper mapper,
            ILogger<CustomerProfileService> logger)
        {
            _histogram = MetricsHelper.GetHistogram("customer_profile_api_data_request",
                "Customer Profile API Data Retrieval Requests", "provider");
            _counter = MetricsHelper.GetCounter("customer_profile_api_data_response",
                "Customer Profile API Data Response", "provider", "keyword");
            _esCustomerProviderService = esCustomerProviderService;
            _dataAccessProvider = dataAccessProvider;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetCustomersById(IEnumerable<Guid> ids, string correlationId,
            DataModels.CustomerResponseOptions customerResponseOption)
        {
            if (ids == null) return new List<Customer>();

            var provider = _dataAccessProvider.GetType().Name
                .Replace("DataProvider", "", StringComparison.InvariantCultureIgnoreCase);
            using (_histogram.WithLabels(provider).NewTimer())
            {
                var customerQuery = new DataModels.CustomerQuery
                {
                    Ids = ids
                };
                var results =
                    await _dataAccessProvider.GetCustomersAsync(customerQuery, correlationId, customerResponseOption);
                var resultIds = results.Select(x => x.Id);
                _logger.LogInformation(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: DB search by id, info: {SerilogProperty.Detail}.",
                    nameof(CustomerProfileService),
                    nameof(GetCustomersById),
                    new
                    {
                        customerIds = resultIds.ToJsonString(),
                        customerIdResults = results.Select(SerilogHelper.LogSensitiveCustomerInfo).ToJsonString()
                    });
                _counter.WithLabels(provider, string.Empty).Inc(results.Count());
                return results;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomersByMobile(string mobile, string correlationId,
            params string[] fields)
        {
            var query = new ElasticSearchQueryModel
            {
                Must = new QueryByMobile(mobile)
            };
            var customerResponseOption = new DataModels.CustomerResponseOptions
            {
                IsIncludedSocialMediaS3 = true
            };
            var result = await QueryFromElasticSearch(mobile, query, correlationId, customerResponseOption, fields);
            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByComparableAddress(string residentialComparableAddress,
            string correlationId, params string[] fields)
        {
            var query = new ElasticSearchQueryModel
            {
                Must = new QueryByComparableAddress(residentialComparableAddress)
            };

            var customerResponseOption = new DataModels.CustomerResponseOptions();
            var result = await QueryFromElasticSearch(residentialComparableAddress, query, correlationId,
                customerResponseOption, fields);
            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByVedaReferenceNumber(string vedaReferenceNumber,
            string correlationId, params string[] fields)
        {
            if (string.IsNullOrWhiteSpace(vedaReferenceNumber))
                return new List<Customer>();

            var query = new ElasticSearchQueryModel
            {
                Must = new QueryByVedaReferenceNumber(vedaReferenceNumber)
            };

            var customerResponseOption = new DataModels.CustomerResponseOptions();

            _logger.LogInformation(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: Begin search by Veda reference number: {SerilogProperty.Detail}.",
                    nameof(CustomerProfileService),
                    nameof(GetCustomersByVedaReferenceNumber),
                    vedaReferenceNumber);

            var result = await QueryFromElasticSearch(vedaReferenceNumber, query, correlationId, customerResponseOption,
                fields);

            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByKeyword(string keyword, string correlationId,
            params string[] fields)
        {
            IEnumerable<CustomerEntity> results;
            var resultIds = new List<Guid>();
            using (_histogram.WithLabels("ElasticSearch").NewTimer())
            {
                results = await _esCustomerProviderService.SearchDocumentsAsync(keyword, correlationId);

                if (!results.Any())
                {
                    _logger.LogInformation(
                        $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: No results ES search by keyword: {SerilogProperty.Detail}.",
                        nameof(CustomerProfileService),
                        nameof(GetCustomersByKeyword),
                        keyword);
                    return new List<Customer>();
                }

                foreach (var item in results) resultIds.Add(new Guid(item.Id));

                _logger.LogInformation(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: parsed the results ES search by keyword: '{keyword}', info: {SerilogProperty.Detail}.",
                    nameof(CustomerProfileService),
                    nameof(GetCustomersByKeyword),
                    new
                    {
                        customerIds = resultIds.ToJsonString(),
                        customerIdResults = results.Select(SerilogHelper.LogSensitiveCustomerInfo).ToJsonString()
                    });
                _counter.WithLabels("ElasticSearch", keyword.GetHashSHA256()).Inc(results.Count());
            }

            // return result from ES if already received requested data
            if (fields.All(f => _fieldsReadableFormEs.Contains(f)))
                return results.Select(doc => _mapper.Map<Customer>(doc));

            var customerResponseOption = new DataModels.CustomerResponseOptions
            {
                IsIncludedSocialMediaS3 = fields.Any(x =>
                    x.Contains(S3SocialMediaRequest, StringComparison.InvariantCultureIgnoreCase))
            };

            return await GetCustomersById(resultIds, correlationId, customerResponseOption);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByPaypalId(string paypalId, string correlationId,
            params string[] fields)
        {
            var customerQuery = new DataModels.CustomerQuery
            {
                PaypayId = paypalId
            };
            var result = await QueryFromDataProvider(paypalId, customerQuery, correlationId, fields);

            return result;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByFacebookId(string facebookId, string correlationId,
            params string[] fields)
        {
            var customerQuery = new DataModels.CustomerQuery
            {
                FacebookId = facebookId
            };
            var result = await QueryFromDataProvider(facebookId, customerQuery, correlationId, fields);

            return result;
        }

        private async Task<IEnumerable<Customer>> QueryFromElasticSearch(string queryText,
            ElasticSearchQueryModel esQuery, string correlationId,
            DataModels.CustomerResponseOptions customerResponseOption, params string[] fields)
        {
            if (string.IsNullOrWhiteSpace(queryText)) return new List<Customer>();

            IEnumerable<CustomerEntity> results;
            IEnumerable<Guid> resultIds;

            using (_histogram.WithLabels("ElasticSearch").NewTimer())
            {
                results = await _esCustomerProviderService.SearchDocumentsAsync(esQuery, correlationId);
                resultIds = results.Select(x => new Guid(x.Id));
                _logger.LogInformation(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: ES search, info: {SerilogProperty.Detail}.",
                    nameof(CustomerProfileService),
                    nameof(QueryFromElasticSearch),
                    new
                    {
                        queryText,
                        correlationId,
                        customerIds = resultIds.ToJsonString(),
                        customerIdResults = results.Select(SerilogHelper.LogSensitiveCustomerInfo).ToJsonString()
                    });
                _counter.WithLabels("ElasticSearch", queryText.GetHashSHA256()).Inc(results.Count());
            }

            if (!results.Any()) return new List<Customer>();

            if (fields.All(f => _fieldsReadableFormEs.Contains(f)))
                // return result from ES if already received requested data
                return results.Select(doc => _mapper.Map<Customer>(doc));

            return await GetCustomersById(resultIds, correlationId, customerResponseOption);
        }

        private async Task<IEnumerable<Customer>> QueryFromDataProvider(string queryText,
            DataModels.CustomerQuery query, string correlationId, params string[] fields)
        {
            if (string.IsNullOrEmpty(queryText))
                return new List<Customer>();

            var provider = _dataAccessProvider.GetType().Name
                .Replace("DataProvider", "", StringComparison.InvariantCultureIgnoreCase);

            using (_histogram.WithLabels(provider).NewTimer())
            {
                var customerResponseOption = new DataModels.CustomerResponseOptions
                {
                    IsIncludedSocialMediaS3 = fields.Any(x =>
                        x.Contains(S3SocialMediaRequest, StringComparison.InvariantCultureIgnoreCase))
                };

                var results = await _dataAccessProvider.GetCustomersAsync(query, correlationId, customerResponseOption);
                var resultIds = results.Select(x => x.Id);
                _logger.LogInformation(
                    $"{SerilogProperty.ClassName}.{SerilogProperty.MethodName}:: DB search , info: {SerilogProperty.Detail}.",
                    nameof(CustomerProfileService),
                    nameof(QueryFromDataProvider),
                    new
                    {
                        correlationId,
                        customerIds = resultIds.ToJsonString(),
                        customerIdResults = results.Select(SerilogHelper.LogSensitiveCustomerInfo).ToJsonString()
                    });

                _counter.WithLabels(provider, string.Empty).Inc(results.Count());

                return results;
            }
        }
    }
}