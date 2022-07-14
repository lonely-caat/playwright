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
using Zip.MerchantDataEnrichment.Application.MerchantFees.Command.PatchFeeReference;
using Zip.MerchantDataEnrichment.Domain.Entities;

namespace Zip.Api.MerchantDataEnrichment.Test.UnitTests.Commands.MerchantFees
{
    public class PatchFeeReferenceCommandHandlerTests : CommonTestsFixture
    {
        private readonly PatchFeeReferenceCommandHandler _target;

        public PatchFeeReferenceCommandHandlerTests()
        {
            var logger = new Mock<ILogger<PatchFeeReferenceCommandHandler>>();
            _target = new PatchFeeReferenceCommandHandler(logger.Object,
                                                          Mapper,
                                                          DbContext);
        }

        [Theory, AutoData]
        public async Task When_FeeReference_No_Exists_Handle_Should_Throw(PatchFeeReferenceCommand request)
        {
            // Arrange & Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken); };

            // Assert
            await func.Should().ThrowAsync<CustomException>();
        }

        [Theory, AutoData]
        public async Task Given_FeeReference_Handle_Should_Work_Correctly(PatchFeeReferenceCommand request)
        {
            // Arrange
            var feeReference = Fixture.Build<FeeReference>()
                                      .With(x => x.WebhookId, request.WebhookId)
                                      .Create();

            var entity = (await DbContext.FeeReferences.AddAsync(feeReference, CancellationToken)).Entity;
            await DbContext.SaveChangesAsync(CancellationToken);

            // Act
            var actual = await _target.Handle(request, CancellationToken);
            entity = await DbContext.FeeReferences.FindAsync(entity.Id);

            // Assert
            actual.FeeReferenceId.Should().Be(entity.Id);
            entity.WebhookId.Should().Be(request.WebhookId);
            entity.TangoTransactionId.Should().Be(request.TangoTransactionId);
            entity.TangoThreadId.Should().Be(request.TangoThreadId);
            entity.TransactionHistoryId.Should().Be(request.TransactionHistoryId);
            entity.UpdatedDateTime.Should().NotBeNull();
        }
    }
}