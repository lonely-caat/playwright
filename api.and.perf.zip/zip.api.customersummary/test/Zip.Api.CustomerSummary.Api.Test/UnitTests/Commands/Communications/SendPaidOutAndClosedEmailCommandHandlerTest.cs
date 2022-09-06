using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPaidOutAndClosedEmail;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.CommunicationsService.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Commands.Communications
{
    public class SendPaidOutAndClosedEmailCommandHandlerTest
    {
        private readonly Mock<ICommunicationsService> _paidOutCloseService;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ICreditProfileContext> _creditProfileContext;
        private readonly Fixture _fixture;
        private readonly SendPaidOutAndClosedEmailCommandHandler _target;

        public SendPaidOutAndClosedEmailCommandHandlerTest()
        {
            _paidOutCloseService = new Mock<ICommunicationsService>();
            _mediator = new Mock<IMediator>();
            _creditProfileContext = new Mock<ICreditProfileContext>();
            _fixture = new Fixture();
            _target = new SendPaidOutAndClosedEmailCommandHandler(_paidOutCloseService.Object,
                                                                    _mediator.Object,
                                                                    _creditProfileContext.Object);
        }

        [Fact]
        public void Given_NullInjection_Should()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new SendPaidOutAndClosedEmailCommandHandler(null,
                                                            _mediator.Object,
                                                            _creditProfileContext.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new SendPaidOutAndClosedEmailCommandHandler(_paidOutCloseService.Object,
                                                            null,
                                                            _creditProfileContext.Object);
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new SendPaidOutAndClosedEmailCommandHandler(_paidOutCloseService.Object,
                                                            _mediator.Object,
                                                            null);
            });
        }

        [Fact]
        public async Task Given_Valid_Input_Should_Invoke_Correctly()
        {
            // Arrange
            var validInput = _fixture.Create<SendPaidOutAndClosedEmailCommand>();
            var response = _fixture.Create<CommunicationApiResponse>();
            response.Success = true;

            _paidOutCloseService.Setup(x => x.SendPaidOutCloseEmailAsync(It.IsAny<PaidOutAndClosedEmail>()))
                           .Returns(Task.FromResult(response));

            _creditProfileContext.Setup(x => x.GetProfileDateOfClosureAsync(It.IsAny<long>()))
                            .Returns(Task.FromResult(new string("9/28/2020 2:45:16 PM")));


            var LinkedAccount = _fixture.Create<AccountInfo>();
            var Address = _fixture.Create<Address>();
            var Country = _fixture.Create<Country>();
            var Merchant = _fixture.Create<Merchant>();
            var Gender = _fixture.Create<Gender>();
            var Document = _fixture.Create<IEnumerable<Document>>();
            var LinkedConsumer = _fixture.Build<Consumer>()
                                         .Without(x => x.LinkedConsumer)
                                         .Create();
            var consumerDetails = _fixture.Build<Consumer>()
                                     .With(x => x.LinkedAccount, LinkedAccount)
                                     .With(x => x.LinkedConsumer, LinkedConsumer)
                                     .With(x => x.Address, Address)
                                     .With(x => x.Country, Country)
                                     .With(x => x.ReferredBy, Merchant)
                                     .With(x => x.Gender, Gender)
                                     .With(x => x.Documents, Document)
                                     .Create();
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
            .Returns(Task.FromResult(consumerDetails));

            var contactDetails = _fixture.Create<ContactDto>();
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), default))
            .Returns(Task.FromResult(contactDetails));


            var AccountInfo = _fixture.Create<AccountInfo>();
            var AccountType = _fixture.Create<AccountType>();
            var LmsAccount = _fixture.Create<LmsAccountDto>();
            var accountDetails = _fixture.Build<GetAccountInfoQueryResult>()
                                     .With(x => x.AccountInfo, AccountInfo)
                                     .With(x => x.AccountType, AccountType)
                                     .With(x => x.LmsAccount, LmsAccount)
                                     .Create();
            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
            .Returns(Task.FromResult(accountDetails));

            // Action
            var result = await _target.Handle(validInput, CancellationToken.None);

            Assert.True(result);
        }

        [Fact]
        public async Task Given_InValid_Input_Should_Invoke_Empty()
        {
            // Arrange
            var validInput = _fixture.Create<SendPaidOutAndClosedEmailCommand>();
            var response = _fixture.Create<CommunicationApiResponse>();
            response.Success = true;
            _paidOutCloseService.Setup(x => x.SendPaidOutCloseEmailAsync(It.IsAny<PaidOutAndClosedEmail>()))
                           .Returns(Task.FromResult(response));

            _creditProfileContext.Setup(x => x.GetProfileDateOfClosureAsync(It.IsAny<long>()))
                            .Returns(Task.FromResult(new string("9/28/2020 2:45:16 PM")));


            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
            .Returns(Task.FromResult<Consumer>(null));

            var contactDetails = _fixture.Create<ContactDto>();
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), default))
            .Returns(Task.FromResult(contactDetails));


            var AccountInfo = _fixture.Create<AccountInfo>();
            var AccountType = _fixture.Create<AccountType>();
            var LmsAccount = _fixture.Create<LmsAccountDto>();
            var accountDetails = _fixture.Build<GetAccountInfoQueryResult>()
                                     .With(x => x.AccountInfo, AccountInfo)
                                     .With(x => x.AccountType, AccountType)
                                     .With(x => x.LmsAccount, LmsAccount)
                                     .Create();
            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
            .Returns(Task.FromResult(accountDetails));

            // Action
            var result = await _target.Handle(validInput, CancellationToken.None);

            Assert.False(result);
        }

        [Fact]
        public async Task Given_InValid_Input_Should_Throw_Exception()
        {
            // Arrange
            var validInput = _fixture.Create<SendPaidOutAndClosedEmailCommand>();

            _paidOutCloseService.Setup(x => x.SendPaidOutCloseEmailAsync(It.IsAny<PaidOutAndClosedEmail>()))
                           .ThrowsAsync(new Exception());

            _creditProfileContext.Setup(x => x.GetProfileDateOfClosureAsync(It.IsAny<long>()))
                            .Returns(Task.FromResult(new string("9/28/2020 2:45:16 PM")));

            var LinkedAccount = _fixture.Create<AccountInfo>();
            var Address = _fixture.Create<Address>();
            var Country = _fixture.Create<Country>();
            var Merchant = _fixture.Create<Merchant>();
            var Gender = _fixture.Create<Gender>();
            var Document = _fixture.Create<IEnumerable<Document>>();
            var LinkedConsumer = _fixture.Build<Consumer>()
                                         .Without(x => x.LinkedConsumer)
                                         .Create();
            var consumerDetails = _fixture.Build<Consumer>()
                                     .With(x => x.LinkedAccount, LinkedAccount)
                                     .With(x => x.LinkedConsumer, LinkedConsumer)
                                     .With(x => x.Address, Address)
                                     .With(x => x.Country, Country)
                                     .With(x => x.ReferredBy, Merchant)
                                     .With(x => x.Gender, Gender)
                                     .With(x => x.Documents, Document)
                                     .Create();
            _mediator.Setup(x => x.Send(It.IsAny<GetConsumerQuery>(), default))
            .Returns(Task.FromResult(consumerDetails));

            var contactDetails = _fixture.Create<ContactDto>();
            _mediator.Setup(x => x.Send(It.IsAny<GetContactQuery>(), default))
            .Returns(Task.FromResult(contactDetails));


            var AccountInfo = _fixture.Create<AccountInfo>();
            var AccountType = _fixture.Create<AccountType>();
            var LmsAccount = _fixture.Create<LmsAccountDto>();
            var accountDetails = _fixture.Build<GetAccountInfoQueryResult>()
                                     .With(x => x.AccountInfo, AccountInfo)
                                     .With(x => x.AccountType, AccountType)
                                     .With(x => x.LmsAccount, LmsAccount)
                                     .Create();
            _mediator.Setup(x => x.Send(It.IsAny<GetAccountInfoQuery>(), default))
            .Returns(Task.FromResult(accountDetails));

            // Action
            await Assert.ThrowsAsync<Exception>(() =>
            {
                return _target.Handle(validInput, CancellationToken.None);
            });
        }
    }
}