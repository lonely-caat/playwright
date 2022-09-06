using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;
using Zip.Api.CustomerSummary.Application.Statements.Models;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Statements
{
    public class GenerateStatementCommandHandlerTest : CommonTestsFixture
    {
        private readonly Mock<IMessageLogContext> _messageLogContext;
        private readonly Mock<IAccountContext> _accountContext;
        private readonly Mock<IProductContext> _productContext;
        private readonly Mock<IStatementsService> _statementService;
        private readonly GenerateStatementCommandHandler _target;

        public GenerateStatementCommandHandlerTest()
        {
            _messageLogContext = new Mock<IMessageLogContext>();
            _accountContext = new Mock<IAccountContext>();
            _productContext = new Mock<IProductContext>();
            _statementService = new Mock<IStatementsService>();

            _target = new GenerateStatementCommandHandler(Mapper,
                                                          _messageLogContext.Object,
                                                          _accountContext.Object,
                                                          _productContext.Object,
                                                          _statementService.Object);
        }
        
        [Fact]
        public async Task Given_AccountNotFound_Handle_Should_Throw_Exception()
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementCommand>();
            _accountContext.Setup(x => x.GetAsync(request.AccountId))
                           .ReturnsAsync(null as Account);

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken.None); };

            // Assert
            await func.Should().ThrowAsync<AccountNotFoundException>();
        }

        [Fact]
        public async Task Given_StatementDateNotFound_Handle_Should_Throw_Exception()
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementCommand>();
            var account = Fixture.Build<Account>()
                                 .Without(x => x.StatementDate)
                                 .Create();
            _accountContext.Setup(x => x.GetAsync(request.AccountId))
                           .ReturnsAsync(account);

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken.None); };

            // Assert
            await func.Should().ThrowAsync<StatementDateNotFoundException>();
        }

        [Theory]
        [InlineData(AccountStatus.Dormant)]
        [InlineData(AccountStatus.Closed)]
        [InlineData(AccountStatus.ChargedOff)]
        [InlineData(AccountStatus.ContractAccepted)]
        [InlineData(AccountStatus.Pending)]
        public async Task Given_Account_Is_Not_Active_Handle_Should_Throw_Exception(AccountStatus accountStatus)
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementCommand>();
            var account = Fixture.Build<Account>()
                                 .With(x => x.AccountStatus, accountStatus)
                                 .Create();
            _accountContext.Setup(x => x.GetAsync(request.AccountId))
                           .ReturnsAsync(account);
            _productContext.Setup(x => x.GetAsync(account.AccountType.ProductId))
                           .ReturnsAsync(Fixture.Create<Product>());

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken.None); };

            // Assert
            await func.Should().ThrowAsync<InvalidAccountStatusException>();
        }

        [Theory]
        [InlineData(AccountStatus.Active)]
        [InlineData(AccountStatus.Operational)]
        [InlineData(AccountStatus.Locked)]
        public async Task Given_Account_Is_Active_ProductNotFound_Handle_Should_Throw_Exception(AccountStatus accountStatus)
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementCommand>();
            var account = Fixture.Build<Account>()
                                 .With(x => x.AccountStatus, accountStatus)
                                 .Create();
            _accountContext.Setup(x => x.GetAsync(request.AccountId))
                           .ReturnsAsync(account);
            _productContext.Setup(x => x.GetAsync(account.AccountType.ProductId))
                           .ReturnsAsync(null as Product);

            // Act
            Func<Task> func = async () => { await _target.Handle(request, CancellationToken.None); };

            // Assert
            await func.Should().ThrowAsync<ProductNotFoundException>();
        }

        [Theory]
        [InlineData(true, MessageLogStatus.Sent)]
        [InlineData(false, MessageLogStatus.SendFailed)]
        public async Task Given_Valid_Input_Handle_Should_Invoke_GenerateStatementsAsync_Correctly_And_Insert_MessageLog(bool isSuccessful, MessageLogStatus expectedMessageLogStatus)
        {
            // Arrange
            var request = Fixture.Create<GenerateStatementCommand>();
            var account = Fixture.Build<Account>()
                                 .With(x => x.Id, request.AccountId)
                                 .With(x => x.AccountStatus, AccountStatus.Active)
                                 .With(x => x.StatementDate, request.EndDate)
                                 .Create();
            var product = Fixture.Create<Product>();
            var expected = Fixture.Build<GenerateStatementResponse>()
                                  .With(x => x.IsSuccessful, isSuccessful)
                                  .Create();

            _accountContext.Setup(x => x.GetAsync(request.AccountId))
                           .ReturnsAsync(account);
            
            _productContext.Setup(x => x.GetAsync(account.AccountType.ProductId))
                           .ReturnsAsync(product);
            
            _statementService.Setup(x => x.GenerateStatementsAsync(It.IsAny<GenerateStatementsRequest>(), CancellationToken.None))
                             .ReturnsAsync(isSuccessful);

            // Act
            var actual = await _target.Handle(request, CancellationToken.None);

            // Assert
            _statementService.Verify(
                x => x.GenerateStatementsAsync(
                    It.Is<GenerateStatementsRequest>(y => y.Accounts.Single() == account.Id.ToString() &&
                                                          y.Classification == ((byte)product.Classification).ToString() &&
                                                          y.StatementDate == request.EndDate.Date.ToString("yyyy-MM-dd")),
                    CancellationToken.None), Times.Once);

            _messageLogContext.Verify(
                x => x.InsertAsync(
                    request.ConsumerId,
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    string.Empty,
                    It.Is<MessageLogSettings>(
                        y => y.DeliveryMethod == MessageLogDeliveryMethod.Email &&
                             y.Category == MessageLogCategory.Consumer &&
                             y.Type == MessageLogType.Statement &&
                             y.Status == expectedMessageLogStatus),
                    It.IsAny<DateTime>(),
                    null), Times.Once);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
