using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateAddress;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateDateOfBirth;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateGender;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateName;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer
{
    public class UpdateConsumerCommandHandler : IRequestHandler<UpdateConsumerCommand>
    {
        private readonly IMediator _mediator;

        public UpdateConsumerCommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Unit> Handle(UpdateConsumerCommand request, CancellationToken cancellationToken)
        {
            var consumer = await _mediator.Send(new GetConsumerQuery(request.ConsumerId));

            if (consumer == null)
            {
                throw new ConsumerNotFoundException($"Cannot find existing customer with consumer id = ${request.ConsumerId}");
            }

            var accountInfoQueryResult = await GetAccountInfoAsync(request.ConsumerId);

            if (IsNameChanged(consumer, request))
            {
                await _mediator.Send(new UpdateNameCommand(request.ConsumerId, request.FirstName, request.LastName, accountInfoQueryResult?.AccountInfo, consumer));
            }

            if (IsDateOfBirthChanged(consumer, request))
            {
                await _mediator.Send(new UpdateDateOfBirthCommand(consumer, accountInfoQueryResult?.AccountInfo, request.DateOfBirth.ToLocalTime()));
            }

            if (IsAddressChanged(consumer, request))
            {
                await _mediator.Send(new UpdateAddressCommand(request.ConsumerId, request.Address));
            }

            if (IsGenderChanged(consumer, request))
            {
                await _mediator.Send(new UpdateGenderCommand(request.ConsumerId, request.Gender));
            }

            return Unit.Value;
        }

        private async Task<GetAccountInfoQueryResult> GetAccountInfoAsync(long consumerId)
        {
            try
            {
                return await _mediator.Send(new GetAccountInfoQuery(consumerId));
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"UpdateConsumerCommandHandler :: GetAccountInfoAsync : {consumerId}");
                return null;
            }
        }

        private bool IsGenderChanged(Consumer personalInfo, UpdateConsumerCommand request)
        {
            return personalInfo.Gender != request.Gender;
        }

        private bool IsAddressChanged(Consumer personalInfo, UpdateConsumerCommand request)
        {
            return personalInfo.Address != request.Address;
        }

        private bool IsDateOfBirthChanged(Consumer personalInfo, UpdateConsumerCommand request)
        {
            return personalInfo.DateOfBirth.Date != request.DateOfBirth.Date;
        }

        private bool IsNameChanged(Consumer personalInfo, UpdateConsumerCommand request)
        {
            return personalInfo.FirstName != request.FirstName || personalInfo.LastName != request.LastName;
        }
    }
}
