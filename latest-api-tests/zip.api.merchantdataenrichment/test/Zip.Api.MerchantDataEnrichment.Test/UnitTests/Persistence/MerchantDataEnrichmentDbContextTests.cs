using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Persistence
{
    public class MerchantDataEnrichmentDbContextTests : CommonTestsFixture
    {
        [Fact]
        public async Task Given_MerchantDetail_DbContext_Should_Create_Correctly()
        {
            // Arrange
            var merchantDetail = Fixture.Build<MerchantDetail>().Without(x => x.VcnTransactions).Create();

            // Act
            await DbContext.MerchantDetails.AddAsync(merchantDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            var actual = await DbContext.MerchantDetails.FindAsync(merchantDetail.Id);

            // Assert
            actual.Should().BeEquivalentTo(merchantDetail);
        }

        [Fact]
        public async Task Given_VcnTransaction_DbContext_Should_Create_Correctly()
        {
            // Arrange
            var vcnTransaction = Fixture.Build<VcnTransaction>()
                                        .Without(x => x.MerchantDetail)
                                        .Without(x => x.MerchantDetailId)
                                        .Create();
            // Act
            await DbContext.VcnTransactions.AddAsync(vcnTransaction);
            await DbContext.SaveChangesAsync(CancellationToken);

            var actual = await DbContext.VcnTransactions.FindAsync(vcnTransaction.Id);

            // Assert
            actual.Should().BeEquivalentTo(vcnTransaction);
        }

        [Fact]
        public async Task Given_VcnTransaction_And_MerchantDetail_DbContext_Should_Create_Correctly()
        {
            // Arrange
            var vcnTransaction = Fixture.Build<VcnTransaction>()
                                        .Without(x => x.MerchantDetail)
                                        .Without(x => x.MerchantDetailId)
                                        .Create();

            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .Create();

            // Act
            await DbContext.VcnTransactions.AddAsync(vcnTransaction);
            await DbContext.SaveChangesAsync(CancellationToken);

            var savedVcnTransaction = await DbContext.VcnTransactions.FindAsync(vcnTransaction.Id);
            savedVcnTransaction.MerchantDetailId = merchantDetail.Id;

            await DbContext.MerchantDetails.AddAsync(merchantDetail);
            await DbContext.SaveChangesAsync(CancellationToken);

            var actualVcnTransaction = await DbContext.VcnTransactions.FindAsync(vcnTransaction.Id);
            var actualMerchantDetail = await DbContext.MerchantDetails.FindAsync(merchantDetail.Id);

            // Assert
            actualVcnTransaction.MerchantDetailId.Should().Be(merchantDetail.Id);
            actualVcnTransaction.MerchantDetail.Should().BeEquivalentTo(merchantDetail);
            actualMerchantDetail.VcnTransactions.SingleOrDefault().Should().BeEquivalentTo(savedVcnTransaction);
        }
    }
}
