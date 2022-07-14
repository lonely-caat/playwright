using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.CreateMerchantLink;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.MerchantLinks
{
    public class CreateMerchantLinkCommandHandlerTests : CommonTestsFixture
    {
        private readonly CreateMerchantLinkCommandHandler _target;

        public CreateMerchantLinkCommandHandlerTests()
        {
            var logger = new Mock<ILogger<CreateMerchantLinkCommandHandler>>();
            _target = new CreateMerchantLinkCommandHandler(logger.Object,
                                                           Mapper,
                                                           DbContext);
        }

        [Theory, AutoData]
        public async Task Given_MerchantDetail_Not_Exists_Handle_Should_Throw(CreateMerchantLinkCommand request)
        {
            // Arrange & Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Unique_Constraint_Violated_Handle_Should_Throw()
        {
            // Arrange
            var merchantLink = Fixture.Create<MerchantLink>();
            var merchantLinkEntity = DbContext.MerchantLinks.Add(merchantLink).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Dummy case where we want to update ML1 with detail of ML2
            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.MerchantDetailId, merchantLinkEntity.MerchantDetailId)
                                 .With(x => x.ZipMerchantId, merchantLinkEntity.ZipMerchantId)
                                 .With(x => x.ZipBranchId, merchantLinkEntity.ZipBranchId)
                                 .With(x => x.ZipCompanyId, merchantLinkEntity.ZipCompanyId)
                                 .With(x => x.MerchantAccountHash, merchantLinkEntity.MerchantAccountHash)
                                 .With(x => x.Active, true)
                                 .Create();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Valid_Data_Handle_Should_Create_MerchantLink_Correctly()
        {
            // Arrange
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantCardAcceptorLocators)
                                        .Without(x => x.MerchantLinks)
                                        .Create();
            var merchantDetailEntity = DbContext.MerchantDetails.Add(merchantDetail).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<CreateMerchantLinkCommand>()
                                 .With(x => x.MerchantDetailId, merchantDetailEntity.Id)
                                 .Without(x => x.Active)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            var entity = await DbContext.MerchantLinks.FindAsync(actual.MerchantLink.Id);

            // Assert
            actual.MerchantLink.MerchantDetailId.Should().Be(merchantDetailEntity.Id);
            actual.MerchantLink.ZipMerchantId.Should().Be(request.ZipMerchantId);
            actual.MerchantLink.ZipBranchId.Should().Be(request.ZipBranchId);
            actual.MerchantLink.ZipCompanyId.Should().Be(request.ZipCompanyId);
            actual.MerchantLink.MerchantAccountHash.Should().Be(request.MerchantAccountHash);
            actual.MerchantLink.Active.Should().BeTrue();

            entity.MerchantDetailId.Should().Be(merchantDetailEntity.Id);
            entity.MerchantDetail.Should().BeEquivalentTo(merchantDetailEntity);
        }
    }
}