using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.Reports.Query.GetMerchantDemographicReport;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Domain.Enums;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Queries.Reports
{
    public class GetMerchantDemographicReportQueryHandlerTests : CommonTestsFixture
    {
        private readonly GetMerchantDemographicReportQueryHandler _target;

        public GetMerchantDemographicReportQueryHandlerTests()
        {
            var logger = new Mock<ILogger<GetMerchantDemographicReportQueryHandler>>();
            _target = new GetMerchantDemographicReportQueryHandler(logger.Object,
                                                                   Mapper,
                                                                   DbContext);
        }

        [Fact]
        public async Task Given_Data_Report_Should_Be_Correct()
        {
            // Arrange
            var request = Fixture.Build<GetMerchantDemographicReportQuery>()
                                 .With(x => x.StartDateTime, DateTime.Now.AddMonths(-2))
                                 .With(x => x.EndDateTime, DateTime.Now)
                                 .Create();
            await PrepareTestingDataAsync(request.ABN, request.Postcode);

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.Should().NotBeNull();
            actual.TotalCustomerCount.Should().Be(2);
            actual.TotalTransactionCount.Should().Be(6);
            actual.TotalTransactionAmount.Should().Be(99.99M);

            actual.AgeGroupDetails.Should().HaveCount(2);
            actual.AgeGroupDetails[0].TotalCustomerCount.Should().Be(1);
            actual.AgeGroupDetails[0].TotalTransactionCount.Should().Be(3);
            actual.AgeGroupDetails[0].TotalTransactionAmount.Should().Be(33.33M);
            actual.AgeGroupDetails[1].TotalCustomerCount.Should().Be(1);
            actual.AgeGroupDetails[1].TotalTransactionCount.Should().Be(3);
            actual.AgeGroupDetails[1].TotalTransactionAmount.Should().Be(66.66M);

            actual.GenderGroupDetails.Should().HaveCount(2);
            actual.GenderGroupDetails[0].TotalCustomerCount.Should().Be(1);
            actual.GenderGroupDetails[0].TotalTransactionCount.Should().Be(3);
            actual.GenderGroupDetails[0].TotalTransactionAmount.Should().Be(66.66M);
            actual.GenderGroupDetails[1].TotalCustomerCount.Should().Be(1);
            actual.GenderGroupDetails[1].TotalTransactionCount.Should().Be(3);
            actual.GenderGroupDetails[1].TotalTransactionAmount.Should().Be(33.33M);

            actual.LocationGroupDetails.Should().HaveCount(2);
            actual.LocationGroupDetails[0].TotalCustomerCount.Should().Be(1);
            actual.LocationGroupDetails[0].TotalTransactionCount.Should().Be(3);
            actual.LocationGroupDetails[0].TotalTransactionAmount.Should().Be(66.66M);
            actual.LocationGroupDetails[1].TotalCustomerCount.Should().Be(1);
            actual.LocationGroupDetails[1].TotalTransactionCount.Should().Be(3);
            actual.LocationGroupDetails[1].TotalTransactionAmount.Should().Be(33.33M);

            // Clean Up
            await CleanUpAsync();
        }

        private async Task PrepareTestingDataAsync(string abn, string postcode)
        {
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.ABN, abn)
                                        .With(x => x.Postcode, postcode)
                                        .Without(x => x.VcnTransactions)
                                        .Create();
            var customerDetail_1 = Fixture.Build<CustomerDetail>()
                                          .With(x => x.DateOfBirth, DateTime.Now.AddYears(-20))
                                          .With(x => x.Gender, Gender.MaleWithKids)
                                          .With(x => x.Suburb, "Strathfield")
                                          .With(x => x.State, "NSW")
                                          .Without(x => x.VcnTransactions)
                                          .Create();
            var transactions_1 = Fixture.Build<VcnTransaction>()
                                        .With(x => x.CustomerDetailId, customerDetail_1.Id)
                                        .With(x => x.AccountId, customerDetail_1.AccountId)
                                        .With(x => x.CustomerId, customerDetail_1.CustomerId)
                                        .With(x => x.TransactionDateTime, DateTime.Now.AddMonths(-1))
                                        .With(x => x.MerchantDetailId, merchantDetail.Id)
                                        .With(x => x.Amount, 11.11M)
                                        .Without(x => x.CustomerDetail)
                                        .Without(x => x.MerchantDetail)
                                        .CreateMany(3);
            var customerDetail_2 = Fixture.Build<CustomerDetail>()
                                          .With(x => x.DateOfBirth, DateTime.Now.AddYears(-30))
                                          .With(x => x.Gender, Gender.FemaleWithKids)
                                          .With(x => x.Suburb, "Castle Hill")
                                          .With(x => x.State, "NSW")
                                          .Without(x => x.VcnTransactions)
                                          .Create();
            var transactions_2 = Fixture.Build<VcnTransaction>()
                                        .With(x => x.CustomerDetailId, customerDetail_2.Id)
                                        .With(x => x.AccountId, customerDetail_2.AccountId)
                                        .With(x => x.CustomerId, customerDetail_2.CustomerId)
                                        .With(x => x.TransactionDateTime, DateTime.Now.AddMonths(-1))
                                        .With(x => x.MerchantDetailId, merchantDetail.Id)
                                        .With(x => x.Amount, 22.22M)
                                        .Without(x => x.CustomerDetail)
                                        .Without(x => x.MerchantDetail)
                                        .CreateMany(3);

            await DbContext.MerchantDetails.AddAsync(merchantDetail);
            await DbContext.VcnTransactions.AddRangeAsync(transactions_1);
            await DbContext.VcnTransactions.AddRangeAsync(transactions_2);
            await DbContext.CustomerDetails.AddRangeAsync(customerDetail_1, customerDetail_2);
            await DbContext.SaveChangesAsync(CancellationToken);
        }

        [Theory]
        [InlineData("22086288888")]
        [InlineData("38167106176")]
        [InlineData("93111195389")]
        public async Task Given_Unsupported_ABN_Handle_Should_Throw_Exception(string abn)
        {
            // Arrange
            var request = Fixture.Build<GetMerchantDemographicReportQuery>()
                                 .With(x => x.ABN, abn)
                                 .Create();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<MerchantDemographicReportUnsupportedException>();
        }

        private async Task CleanUpAsync()
        {
            foreach (var entity in DbContext.VcnTransactions)
            {
                DbContext.VcnTransactions.Remove(entity);
            }

            foreach (var entity in DbContext.CustomerDetails)
            {
                DbContext.CustomerDetails.Remove(entity);
            }

            foreach (var entity in DbContext.MerchantDetails)
            {
                DbContext.MerchantDetails.Remove(entity);
            }

            await DbContext.SaveChangesAsync(CancellationToken);
        }
    }
}
