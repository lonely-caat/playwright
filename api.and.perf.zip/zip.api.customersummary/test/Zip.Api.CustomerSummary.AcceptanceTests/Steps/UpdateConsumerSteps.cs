using AutoFixture;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class UpdateConsumerSteps : BaseStep
    {
        private readonly IFixture _fixture;

        public UpdateConsumerSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
            _fixture = new Fixture();
        }

        [When(@"In order to update names, I make a post request to /consumers with following new names")]
        public async Task WhenInOrderToUpdateNamesIMakeAPostRequestToConsumersWithFollowingNewNames(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var updateConsumerCommand = new UpdateConsumerCommand
                {
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    FirstName = testItem["firstName"],
                    LastName = testItem["lastName"],
                    DateOfBirth = DateTime.Now.Date.AddYears(-20),
                    Address = _fixture.Create<Address>(),
                    CountryName = _fixture.Create<string>(),
                    Gender = _fixture.Create<Gender>()
                };
                
                response = await _httpClient.PutAsync("/api/consumers", updateConsumerCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }

        [When(@"In order to update date of birth, I make a post request to /consumers with following new birthdays")]
        public async Task WhenInOrderToUpdateDateOfBirthIMakeAPostRequestToConsumersWithFollowingNewBirthdays(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var updateConsumerCommand = new UpdateConsumerCommand
                {
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>(),
                    DateOfBirth = DateTime.Parse(testItem["date of birth"]),
                    Address = _fixture.Create<Address>(),
                    CountryName = _fixture.Create<string>(),
                    Gender = _fixture.Create<Gender>()
                };
                
                response = await _httpClient.PutAsync("/api/consumers", updateConsumerCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }


        [When(@"In order to update gender, I make a post request to /consumer with following new genders")]
        public async Task WhenInOrderToUpdateGenderIMakeAPostRequestToConsumerWithFollowingNewGenders(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var updateConsumerCommand = new UpdateConsumerCommand
                {
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    DateOfBirth = DateTime.Now.AddYears(-20),
                    Address = _fixture.Create<Address>(),
                    CountryName = _fixture.Create<string>(),
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>(),
                    Gender = Enum.Parse<Gender>(testItem["gender"])
                };
                
                response = await _httpClient.PutAsync("/api/consumers", updateConsumerCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }

        [When(@"In order to update address, I make a post request to /consumer with following new addresses")]
        public async Task WhenInOrderToUpdateAddressIMakeAPostRequestToConsumerWithFollowingNewAddresses(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var updateConsumerCommand = new UpdateConsumerCommand
                {
                    ConsumerId = long.Parse(testItem["consumerId"]),
                    DateOfBirth = DateTime.Now.AddYears(-20),
                    Address = new Address
                    {
                        Country = string.IsNullOrEmpty(testItem["countryCode"]) ? null : new Country
                        {
                            Id = testItem["countryCode"],
                            Name = testItem["countryCode"] == "AZ" ? "Australia" : "New Zealand"
                        },
                        CountryCode = testItem["countryCode"],
                        PostCode = testItem["postCode"],
                        State = testItem["state"],
                        StreetNumber = testItem["streetNumber"],
                        Suburb = testItem["suburb"],
                        UnitNumber = testItem["unitNumber"],
                        StreetName = testItem["streetName"]
                    },
                    CountryName = _fixture.Create<string>(),
                    FirstName = _fixture.Create<string>(),
                    LastName = _fixture.Create<string>(),
                    Gender = _fixture.Create<Gender>(),
                };
                
                response = await _httpClient.PutAsync("/api/consumers", updateConsumerCommand.ToJsonHttpContent());

                Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
            }
        }

        [Then(@"the response status code should be expected")]
        public void ThenTheResponseStatusCodeShouldBeExpected()
        {
        }
    }
}
