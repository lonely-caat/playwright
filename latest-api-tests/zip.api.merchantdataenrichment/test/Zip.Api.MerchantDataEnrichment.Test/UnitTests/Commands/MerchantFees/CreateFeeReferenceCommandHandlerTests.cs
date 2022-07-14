using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.CreateFeeReference;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.MerchantFees
{
    public class CreateFeeReferenceCommandHandlerTests : CommonTestsFixture
    {
        private readonly CreateFeeReferenceCommandHandler _target;

        public CreateFeeReferenceCommandHandlerTests()
        {
            var logger = new Mock<ILogger<CreateFeeReferenceCommandHandler>>();
            _target = new CreateFeeReferenceCommandHandler(logger.Object,
                                                           Mapper,
                                                           DbContext);
        }

        [Fact]
        public async Task When_MerchantLink_Not_Exists_Handle_Should_Work_Correctly()
        {
            // Arrange & Act
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .With(x => x.WebhookId, Guid.NewGuid().ToString)
                                 .With(x => x.TransactionAmount, "1.23456")
                                 .Create();
            var actual = await _target.Handle(request, CancellationToken);
            var feeReferenceEntity = await DbContext.FeeReferences
                                        .FirstOrDefaultAsync(x => x.WebhookId == request.WebhookId, CancellationToken);

            // Assert
            actual.Should().BeNull();
            feeReferenceEntity.Should().NotBeNull();
            feeReferenceEntity.OriginalTransactionAmount.Should().Be(decimal.Parse(request.TransactionAmount));
        }

        [Fact]
        public async Task Given_MerchantLink_When_OpenLoopProduct_Not_Exists_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .With(x => x.WebhookId, Guid.NewGuid().ToString)
                                 .With(x => x.TransactionAmount, "1.23456")
                                 .Create();
            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                      .Without(x => x.MerchantDetail)
                                      .Create();
            var cardAcceptorLocator = $"{request.CardAcceptorName?.Trim().ToUpperInvariant()} {request.CardAcceptorCity?.Trim().ToUpperInvariant()}";
            var mCal = Fixture.Build<MerchantCardAcceptorLocator>()
                              .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                              .With(x => x.CardAcceptorLocator, cardAcceptorLocator)
                              .Without(x => x.MerchantDetail)
                              .Create();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.Id, new Guid(Constants.MerchantDetailId))
                                        .With(x => x.MerchantCardAcceptorLocators, new List<MerchantCardAcceptorLocator> { mCal })
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantLinks)
                                        .Without(x => x.MerchantOpenLoopProducts)
                                        .Create();

            await DbContext.MerchantLinks.AddAsync(merchantLink, CancellationToken);
            await DbContext.MerchantCardAcceptorLocators.AddAsync(mCal, CancellationToken);
            await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            var feeReferenceEntity = await DbContext.FeeReferences
                                                    .FirstOrDefaultAsync(x => x.WebhookId == request.WebhookId, CancellationToken);

            // Assert
            actual.Should().BeNull();
            feeReferenceEntity.Should().NotBeNull();
            feeReferenceEntity.OriginalTransactionAmount.Should().Be(decimal.Parse(request.TransactionAmount));
        }

        [Fact]
        public async Task Given_MerchantLink_And_OpenLoopProduct_Handle_Should_Work_Correctly()
        {
            // Arrange
            var request = Fixture.Build<CreateFeeReferenceCommand>()
                                 .With(x => x.WebhookId, Guid.NewGuid().ToString)
                                 .With(x => x.TransactionAmount, "1.23456")
                                 .With(x => x.CardAcceptorName, Constants.CardAcceptorName)
                                 .With(x => x.CardAcceptorCity, Constants.CardAcceptorCity)
                                 .Create();
            var merchantLink = Fixture.Build<MerchantLink>()
                                      .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                      .Without(x => x.MerchantDetail)
                                      .Create();
            var cardAcceptorLocator = $"{request.CardAcceptorName?.Trim().ToUpperInvariant()} {request.CardAcceptorCity?.Trim().ToUpperInvariant()}";
            var mCal = Fixture.Build<MerchantCardAcceptorLocator>()
                              .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                              .With(x => x.CardAcceptorLocator, cardAcceptorLocator)
                              .Without(x => x.MerchantDetail)
                              .Create();
            var openLoopProduct = Fixture.Build<OpenLoopProduct>()
                                         .With(x => x.TransactionAmountLowerLimit, 0)
                                         .With(x => x.TransactionAmountUpperLimit, decimal.MaxValue)
                                         .Without(x => x.MerchantOpenLoopProducts)
                                         .Without(x => x.FeeReferences)
                                         .Create();
            var merchantOpenLoopProduct = Fixture.Build<MerchantOpenLoopProduct>()
                                                 .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                                 .With(x => x.ZipMerchantId, merchantLink.ZipMerchantId)
                                                 .With(x => x.OpenLoopProduct, openLoopProduct)
                                                 .Without(x => x.MerchantDetail)
                                                 .Create();
            var merchantDetail = Fixture.Build<MerchantDetail>()
                                        .With(x => x.Id, new Guid(Constants.MerchantDetailId))
                                        .With(x => x.MerchantCardAcceptorLocators, new List<MerchantCardAcceptorLocator> { mCal })
                                        .With(x => x.MerchantOpenLoopProducts, new List<MerchantOpenLoopProduct> { merchantOpenLoopProduct })
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantLinks)
                                        .Create();

            await DbContext.MerchantLinks.AddAsync(merchantLink, CancellationToken);
            await DbContext.MerchantCardAcceptorLocators.AddAsync(mCal, CancellationToken);
            await DbContext.OpenLoopProducts.AddAsync(openLoopProduct, CancellationToken);
            await DbContext.MerchantOpenLoopProducts.AddAsync(merchantOpenLoopProduct, CancellationToken);
            await DbContext.MerchantDetails.AddAsync(merchantDetail, CancellationToken);
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            var feeReferenceEntity = await DbContext.FeeReferences
                                                    .FirstOrDefaultAsync(x => x.WebhookId == request.WebhookId, CancellationToken);

            // Assert
            actual.Should().NotBeNull();
            feeReferenceEntity.Should().NotBeNull();

            actual.MerchantId.Should().Be(feeReferenceEntity.ZipMerchantId);
            actual.BranchId.Should().Be(feeReferenceEntity.ZipBranchId);
            actual.CompanyId.Should().Be(feeReferenceEntity.ZipCompanyId);
            actual.FeeType.Should().Be(feeReferenceEntity.FeeType);
            actual.FeeAmount.Should().Be(feeReferenceEntity.FeeAmount?.ToString("0.00"));
            actual.FeeNarrative.Should().NotBeNullOrEmpty();
            actual.MerchantAccountHash.Should().NotBeNullOrEmpty();
        }
    }
}