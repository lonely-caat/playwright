using System;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerProfile.Interfaces;

namespace Zip.Api.CustomerProfile.Services
{
    public class WarmingService : IWarmingService
    {
        private readonly ICustomerProfileService _customerProfileService;
        private readonly ILogger<WarmingService> _logger;

        public WarmingService(ICustomerProfileService customerProfileService, ILogger<WarmingService> logger)
        {
            _customerProfileService = customerProfileService;
            _logger = logger;
        }

        public void WarmUp()
        {
            _logger.LogInformation("Starting WarmUp actions");

            var fields = new[]
            {
                "id", "givenName", "familyName", "otherName", "mobilePhone", "email", "residentialAddress",
                "driverLicence", "medicare", "applications"
            };
            _customerProfileService.GetCustomersByKeyword(" ", Guid.NewGuid().ToString(), fields).GetAwaiter()
                .GetResult();

            _logger.LogInformation("WarmUp actions are completed.");
        }
    }
}