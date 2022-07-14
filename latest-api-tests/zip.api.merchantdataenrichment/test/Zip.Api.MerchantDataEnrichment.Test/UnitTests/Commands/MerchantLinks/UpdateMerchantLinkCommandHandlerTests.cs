using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.Common.Exceptions;
using Zip.MerchantDataEnrichment.Application.MerchantLinks.Command.UpdateMerchantLink;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.MerchantLinks
{
    public class UpdateMerchantLinkCommandHandlerTests : CommonTestsFixture
    {
        private readonly UpdateMerchantLinkCommandHandler _target;

        public UpdateMerchantLinkCommandHandlerTests()
        {
            var logger = new Mock<ILogger<UpdateMerchantLinkCommandHandler>>();
            _target = new UpdateMerchantLinkCommandHandler(logger.Object,
                                                           Mapper,
                                                           DbContext);
        }

        [Fact]
        public async Task Given_Valid_Data_Handle_Should_Work_Correctly()
        {
            // Arrange
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantCardAcceptorLocators)
                                        .Without(x => x.MerchantLinks)
                                        .Create();
            var merchantDetail2 = Fixture.Build<MerchantDetail>()
                                         .Without(x => x.VcnTransactions)
                                         .Without(x => x.MerchantCardAcceptorLocators)
                                         .Without(x => x.MerchantLinks)
                                         .Create();
            var merchantDetailEntity = DbContext.MerchantDetails.Add(merchantDetail).Entity;
            var merchantDetailEntity2 = DbContext.MerchantDetails.Add(merchantDetail2).Entity;
            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.MerchantDetailId, merchantDetailEntity.Id)
                                      .With(x => x.ZipMerchantId, 1)
                                      .With(x => x.ZipBranchId, 1)
                                      .With(x => x.ZipCompanyId, 1)
                                      .With(x => x.Active, false)
                                      .With(x => x.CreatedDateTime, DateTime.Now.AddDays(-1))
                                      .Without(x => x.UpdatedDateTime)
                                      .Create();
            var merchantLinkEntity = DbContext.MerchantLinks.Add(merchantLink).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, merchantLinkEntity.Id)
                                 .With(x => x.MerchantDetailId, merchantDetailEntity2.Id)
                                 .With(x => x.ZipMerchantId, 2)
                                 .With(x => x.ZipBranchId, 2)
                                 .With(x => x.ZipCompanyId, 2)
                                 .With(x => x.Active, true)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.MerchantLink.Id.Should().Be(merchantLinkEntity.Id);
            actual.MerchantLink.MerchantDetailId.Should().Be(merchantDetailEntity2.Id);
            actual.MerchantLink.ZipMerchantId.Should().Be(request.ZipMerchantId);
            actual.MerchantLink.ZipBranchId.Should().Be(request.ZipBranchId);
            actual.MerchantLink.ZipCompanyId.Should().Be(request.ZipCompanyId);
            actual.MerchantLink.MerchantAccountHash.Should().Be(request.MerchantAccountHash);
            actual.MerchantLink.Active.Should().Be(true);
            actual.MerchantLink.CreatedDateTime.Should().Be(merchantLink.CreatedDateTime);
            actual.MerchantLink.UpdatedDateTime.Should().HaveValue();
        }

        [Fact]
        public async Task Given_Valid_Data_And_Only_Updates_Active_Handle_Should_Work_Correctly()
        {
            // Arrange
            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.Active, false)
                                      .Create();
            var merchantLinkEntity = DbContext.MerchantLinks.Add(merchantLink).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, merchantLinkEntity.Id)
                                 .With(x => x.MerchantDetailId, merchantLinkEntity.MerchantDetailId)
                                 .With(x => x.ZipMerchantId, merchantLinkEntity.ZipMerchantId)
                                 .With(x => x.ZipBranchId, merchantLinkEntity.ZipBranchId)
                                 .With(x => x.ZipCompanyId, merchantLinkEntity.ZipCompanyId)
                                 .With(x => x.MerchantAccountHash, merchantLinkEntity.MerchantAccountHash)
                                 .With(x => x.Active, true)
                                 .Create();

            // Act
            var actual = await _target.Handle(request, CancellationToken);

            // Assert
            actual.MerchantLink.Active.Should().Be(true);
        }

        [Fact]
        public async Task Given_No_MerchantLink_Handle_Should_Throw()
        {
            // Arrange
            var request = Fixture.Create<UpdateMerchantLinkCommand>();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Fact]
        public async Task Given_Unique_Constraint_Violated_Handle_Should_Throw()
        {
            // Arrange
            var merchantLink = Fixture.Create<MerchantLink>();
            var merchantLink2 = Fixture.Create<MerchantLink>();
            var merchantLinkEntity = DbContext.MerchantLinks.Add(merchantLink).Entity;
            var merchantLinkEntity2 = DbContext.MerchantLinks.Add(merchantLink2).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Dummy case where we want to update MLR1 with detail of MLR2
            var request = Fixture.Build<UpdateMerchantLinkCommand>()
                                 .With(x => x.MerchantLinkId, merchantLinkEntity.Id)
                                 .With(x => x.MerchantDetailId, merchantLinkEntity2.MerchantDetailId)
                                 .With(x => x.ZipMerchantId, merchantLinkEntity2.ZipMerchantId)
                                 .With(x => x.ZipBranchId, merchantLinkEntity2.ZipBranchId)
                                 .With(x => x.ZipCompanyId, merchantLinkEntity2.ZipCompanyId)
                                 .With(x => x.Active, true)
                                 .Create();

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }
    }
}
