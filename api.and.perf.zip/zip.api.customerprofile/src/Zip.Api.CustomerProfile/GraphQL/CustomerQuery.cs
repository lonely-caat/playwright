using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GraphQL.Types;
using Microsoft.Extensions.Logging;
using Optional;
using Optional.Unsafe;
using Prometheus;
using Zip.Api.CustomerProfile.GraphQL.Types;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.Api.CustomerProfile.Services;
using Zip.Core.Prometheus;
using Zip.Core.Serilog;
using Zip.CustomerProfile.Contracts;

namespace Zip.Api.CustomerProfile.GraphQL
{
    public class CustomerQuery : ObjectGraphType<object>
    {
        private const string ArgumentKeyword = "keyword";
        private const string ArgumentMobile = "mobile";
        private const string ArgumentId = "id";
        private const string ArgumentPaypalId = "paypalid";
        private const string ArgumentFacebookId = "facebookid";
        public const string ArgumentComparableAddress = "residentialcomparableaddress";
        public const string ArgumentVedaReference = "vedaReferenceNumber";
        private readonly ICustomerProfileService _customerProfileService;
        private readonly Histogram _histogram;
        private readonly ILogger<CustomerQuery> _logger;

        public CustomerQuery(ICustomerProfileService customerProfileService, ILogger<CustomerQuery> logger)
        {
            _histogram = MetricsHelper.GetHistogram("customer_profile_graphql",
                "Customer Profile API GraphQL Requests", "argument");
            _customerProfileService =
                customerProfileService ?? throw new ArgumentNullException(nameof(customerProfileService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Name = "CustomerProfileQuery";

            var queryArguments = GetQueryArguments();

            FieldAsync<ListGraphType<CustomerType>>("customerProfile",
                arguments: queryArguments,
                resolve: async context => await GetCustomerAsync(context));
        }

        private async Task<IEnumerable<Customer>> GetCustomerAsync(ResolveFieldContext<object> context)
        {
            var correlationId = Guid.NewGuid().ToString();

            async Task<Option<IEnumerable<Customer>>> FetchCustomers(string argumentName,
                Func<string, string, string[], Task<IEnumerable<Customer>>> fetchCustomerAsync)
            {
                var result = (await GetCustomersAsync(context, correlationId, argumentName, argumentName,
                    fetchCustomerAsync))?.ToArray() ?? Array.Empty<Customer>();

                if (result == null || result.Length == 0) return Option.None<IEnumerable<Customer>>();

                return Option.Some<IEnumerable<Customer>>(result);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersById()
            {
                return await FetchCustomers(ArgumentId,
                    async (customerId, currentCorrelationId, fields) =>
                        await _customerProfileService.GetCustomersByIdAsync(customerId, currentCorrelationId));
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByPayPalId()
            {
                return await FetchCustomers(ArgumentPaypalId, _customerProfileService.GetCustomersByPaypalId);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByFacebookId()
            {
                return await FetchCustomers(ArgumentFacebookId, _customerProfileService.GetCustomersByFacebookId);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByMobile()
            {
                return await FetchCustomers(ArgumentMobile, _customerProfileService.GetCustomersByMobile);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByComparableAddress()
            {
                return await FetchCustomers(ArgumentComparableAddress,
                    _customerProfileService.GetCustomersByComparableAddress);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByVedaReferenceNumber()
            {
                return await FetchCustomers(ArgumentVedaReference,
                    _customerProfileService.GetCustomersByVedaReferenceNumber);
            }

            async Task<Option<IEnumerable<Customer>>> FetchCustomersByKeyword()
            {
                return await FetchCustomers(ArgumentKeyword,
                    _customerProfileService.GetCustomersByKeyword);
            }

            // Please note: All these methods are async
            var fetchMethods = new Func<Task<Option<IEnumerable<Customer>>>>[]
            {
                FetchCustomersById,
                FetchCustomersByPayPalId,
                FetchCustomersByFacebookId,
                FetchCustomersByMobile,
                FetchCustomersByComparableAddress,
                FetchCustomersByVedaReferenceNumber,
                FetchCustomersByKeyword
            };

            foreach (var fetchCustomersAsync in fetchMethods)
            {
                var currentCustomers = await fetchCustomersAsync();
                if (currentCustomers.HasValue && currentCustomers.ValueOrDefault() != null)
                    return currentCustomers.ValueOrFailure();
            }

            return Enumerable.Empty<Customer>();
        }

        private static QueryArguments GetQueryArguments()
        {
            return new QueryArguments(
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentKeyword
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentMobile
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentId
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentFacebookId
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentPaypalId
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentComparableAddress
                },
                new QueryArgument<StringGraphType>
                {
                    Name = ArgumentVedaReference
                });
        }

        private async Task<IEnumerable<Customer>> GetCustomersAsync(ResolveFieldContext<object> context,
            string correlationId, string histogramLabel, string argumentName,
            Func<string, string, string[], Task<IEnumerable<Customer>>> fetchCustomerAsync)
        {
            var fields = context.SubFields.Select(x => x.Key).ToArray();
            using (_histogram.WithLabels(histogramLabel).NewTimer())
            {
                var argument = context.GetArgument<string>(argumentName);
                if (string.IsNullOrWhiteSpace(argument)) return Enumerable.Empty<Customer>();
                try
                {
                    var results = (await fetchCustomerAsync(argument, correlationId, fields))?.ToArray();
                    if (results != null && results.Any()) return results;
                }
                catch (Exception ex)
                {
                    var methodName = MethodBase.GetCurrentMethod()?.Name ?? string.Empty;
                    _logger.LogError(ex,
                        $"{GetType().FullName}.{methodName} GraphQL query exception thrown. CorrelationId: {correlationId}. Detail: {SerilogProperty.Detail}");
                    throw;
                }
            }

            return Enumerable.Empty<Customer>();
        }
    }
}