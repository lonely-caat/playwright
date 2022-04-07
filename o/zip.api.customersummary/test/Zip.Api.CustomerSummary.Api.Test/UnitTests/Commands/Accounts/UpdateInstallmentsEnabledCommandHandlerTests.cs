using System;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Interfaces;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Accounts
{
    public class UpdateInstallmentsEnabledCommandHandlerTests : CommonTestsFixture
    {
        private readonly UpdateInstallmentsEnabledCommandHandler _target;

        private readonly Mock<IAccountsService> _accountsService;

        private readonly Mock<ILogger<UpdateInstallmentsEnabledCommandHandler>> _logger;

        public UpdateInstallmentsEnabledCommandHandlerTests()
        {
            _accountsService = new Mock<IAccountsService>();
            _logger = new Mock<ILogger<UpdateInstallmentsEnabledCommandHandler>>();
            _target = new UpdateInstallmentsEnabledCommandHandler(_accountsService.Object, _logger.Object);
        }

        [Fact]
        public async Task Given_ValidRequest_Should_Call_AccountsService_UpdateConfiguration()
        {
            // Arrange
            var request = Fixture.Build<UpdateInstallmentsEnabledCommand>()
                                 .With(x => x.AccountId, Fixture.Create<long>())
                                 .With(x => x.AccountTypeId, Fixture.Create<long>())
                                 .With(x => x.IsInstallmentsEnabled, Fixture.Create<bool>())
                                 .Create();

            var configuration = Fixture.Build<AccountResponseConfiguration>()
                                       .With(x => x.InstallmentAccount, request.IsInstallmentsEnabled)
                                       .Create();

            _accountsService.Setup(x => x.UpdateConfiguration(
                                       It.Is<UpdateConfigurationRequest>(
                                           y => y.AccountId == request.AccountId &&
                                                y.AccountTypeId == request.AccountTypeId &&
                                                y.Configuration.InstallmentAccount == request.IsInstallmentsEnabled)))
                            .ReturnsAsync(Fixture.Build<AccountResponse>()
                                                 .With(x => x.Id, request.AccountId.ToString())
                                                 .With(x => x.Configuration, configuration)
                                                 .Create());

            // Act
            var response = await _target.Handle(request, default);

            // Assert
            _accountsService.Verify(x => x.UpdateConfiguration(It.IsAny<UpdateConfigurationRequest>()), Times.Once);

            Assert.NotNull(response);
            Assert.Equal(request.AccountId.ToString(), response.Id);
            Assert.Equal(request.IsInstallmentsEnabled, response.Configuration.InstallmentAccount);
        }

        [Fact]
        public async Task Given_Exception_On_UpdateConfiguration_Should_Throw()
        {
            // Arrange
            var request = Fixture.Build<UpdateInstallmentsEnabledCommand>()
                                 .With(x => x.AccountId, Fixture.Create<long>())
                                 .With(x => x.AccountTypeId, Fixture.Create<long>())
                                 .With(x => x.IsInstallmentsEnabled, Fixture.Create<bool>())
                                 .Create();

            _accountsService.Setup(x => x.UpdateConfiguration(It.IsAny<UpdateConfigurationRequest>()))
                            .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _target.Handle(request, default));
        }
    }
}