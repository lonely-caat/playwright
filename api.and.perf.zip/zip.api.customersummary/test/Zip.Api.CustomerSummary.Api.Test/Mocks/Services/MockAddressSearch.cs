using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockAddressSearch : IAddressSearch
    {
        public Task<GoogleAddress> FindAndVerifyAsync(string addressLine)
        {
            GoogleAddress result = null;

            if (addressLine == "throw")
            {
                throw new Exception("test exception");
            }

            if (addressLine != "no content")
            {
                result = new GoogleAddress()
                {
                    Country = "Australia",
                    CountryCode = Regions.Australia,
                    StreetName = "King street"
                };
            }
            
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Prediction>> SearchAsync(string countryCode, string keyword)
        {
            var result = new List<Prediction>();

            if (keyword == "throw")
            {
                throw new Exception("test exception");
            }

            if (keyword != "no content")
            {
                result.Add(new Prediction()
                {
                    Place_Id = "placeId",
                    Description = "place description"
                });
            }
            return Task.FromResult<IEnumerable<Prediction>>(result);
        }
    }
}