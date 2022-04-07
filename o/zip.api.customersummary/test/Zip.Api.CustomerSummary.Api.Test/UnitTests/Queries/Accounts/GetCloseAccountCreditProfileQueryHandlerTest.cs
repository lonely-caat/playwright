using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Queries.Accounts
{
    public class GetCloseAccountCreditProfileQueryHandlerTest
    {
        private readonly Mock<ICreditProfileContext> creditProfileContext = new Mock<ICreditProfileContext>();

        [Fact]
        public void Given_NullContext_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new GetCloseAccountCreditProfileQueryHandler(null);
            });
        }

        [Fact]
        public async Task Given_NoCreditProfileStateFound_ShouldThrow_CreditProfileNotFoundException()
        {
            creditProfileContext.Setup(x => x.GetStateTypeByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as CreditProfileState);

            await Assert.ThrowsAsync<CreditProfileNotFoundException>(async () =>
           {
               var handler = new GetCloseAccountCreditProfileQueryHandler(creditProfileContext.Object);
               await handler.Handle(new GetCloseAccountCreditProfileQuery(), CancellationToken.None);
           });
        }

        [Fact]
        public async Task Given_CreditProfileStateFound_ShouldReturn_Result()
        {
            creditProfileContext.Setup(x => x.GetStateTypeByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new CreditProfileState()
                {
                    CreditProfileId = 222,
                    CreditStateType = CreditProfileStateType.ApplicationInProgress
                });

            creditProfileContext.Setup(x =>
                x.GetProfileAttributesAsync(
                    It.Is<CreditProfileStateType>(t => t == CreditProfileStateType.ApplicationInProgress)))
                .ReturnsAsync(
                    new List<ProfileAttribute>
                    {
                        new ProfileAttribute() { Id = 234 },
                        new ProfileAttribute() { Id = 456 }
                    });

            creditProfileContext.Setup(x =>
                x.GetProfileClassificationsAsync(
                    It.Is<CreditProfileStateType>(t => t == CreditProfileStateType.ApplicationInProgress)))
                .ReturnsAsync(
                    new List<ProfileClassification>
                    {
                        new ProfileClassification(){ Id = 345},
                        new ProfileClassification(){ Id = 938},
                        new ProfileClassification(){ Id = 392}
                    });

            var handler = new GetCloseAccountCreditProfileQueryHandler(creditProfileContext.Object);
            var result = await handler.Handle(new GetCloseAccountCreditProfileQuery(), CancellationToken.None);

            Assert.Equal(222, result.CreditProfileId);
            Assert.Equal(1, result.NewStateTypes.Count);
        }

        [Fact]
        public async Task Given_CreditProfileStateFound_ShouldCheck_NewStateTypes()
        {
            creditProfileContext.Setup(x => x.GetStateTypeByConsumerIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new CreditProfileState()
                {
                    CreditProfileId = 222,
                    CreditStateType = CreditProfileStateType.ApplicationCompleted
                });

            creditProfileContext.Setup(x =>
                x.GetProfileAttributesAsync(
                    It.IsAny<CreditProfileStateType>()))
                .ReturnsAsync(
                    new List<ProfileAttribute>
                    {
                        new ProfileAttribute() { Id = 234 },
                        new ProfileAttribute() { Id = 456 }
                    });

            creditProfileContext.Setup(x =>
                x.GetProfileClassificationsAsync(
                    It.IsAny<CreditProfileStateType>()))
                .ReturnsAsync(
                    new List<ProfileClassification>
                    {
                        new ProfileClassification(){ Id = 345},
                        new ProfileClassification(){ Id = 938},
                        new ProfileClassification(){ Id = 392}
                    });

            var handler = new GetCloseAccountCreditProfileQueryHandler(creditProfileContext.Object);
            var result = await handler.Handle(new GetCloseAccountCreditProfileQuery(), CancellationToken.None);

            Assert.Equal(222, result.CreditProfileId);
            Assert.Equal(3, result.NewStateTypes.Count);
        }
    }
}
