using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zip.Api.CustomerSummary.Application.Statements.Models;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.StatementService.Models;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement
{
    public class GenerateStatementCommandHandler : IRequestHandler<GenerateStatementCommand, GenerateStatementResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMessageLogContext _messageLogContext;
        private readonly IAccountContext _accountContext;
        private readonly IProductContext _productContext;
        private readonly IStatementsService _statementsService;

        public GenerateStatementCommandHandler(
            IMapper mapper,
            IMessageLogContext messageLogContext,
            IAccountContext accountContext,
            IProductContext productContext,
            IStatementsService statementsService)
        {
            _mapper = mapper;
            _messageLogContext = messageLogContext;
            _accountContext = accountContext;
            _productContext = productContext;
            _statementsService = statementsService;
        }

        public async Task<GenerateStatementResponse> Handle(GenerateStatementCommand request, CancellationToken cancellationToken)
        {
            var product = await GetProductAsync(request.AccountId);
            var generateStatementsRequest = _mapper.Map<GenerateStatementsRequest>(request, opt =>
            {
                opt.Items["Classification"] = ((byte)product.Classification).ToString();
            });

            var isSuccessful = await _statementsService.GenerateStatementsAsync(generateStatementsRequest, cancellationToken);

            var status = isSuccessful ? MessageLogStatus.Sent : MessageLogStatus.SendFailed;

            await _messageLogContext.InsertAsync(request.ConsumerId,
                                                 Guid.NewGuid(),
                                                 "Your zipPay statement is ready",
                                                 string.Empty,
                                                 new MessageLogSettings
                                                 {
                                                     DeliveryMethod = MessageLogDeliveryMethod.Email,
                                                     Category = MessageLogCategory.Consumer,
                                                     Type = MessageLogType.Statement,
                                                     Status = status
                                                 },
                                                 DateTime.Now);

            return new GenerateStatementResponse(isSuccessful);
        }
        
        private async Task<Product> GetProductAsync(long accountId)
        {
            var account = await _accountContext.GetAsync(accountId);

            if (account == null)
            {
                throw new AccountNotFoundException(default, accountId);
            }

            if (!account.StatementDate.HasValue)
            {
                throw new StatementDateNotFoundException(account.Id);
            }

            if (!account.IsActive)
            {
                throw new InvalidAccountStatusException(account.Id);
            }

            var product = await _productContext.GetAsync(account.AccountType.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException(account.AccountType.ProductId);
            }

            return product;
        }
    }
}
