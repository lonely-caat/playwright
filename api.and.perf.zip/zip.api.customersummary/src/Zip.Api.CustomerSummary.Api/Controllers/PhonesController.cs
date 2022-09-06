using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdatePhoneStatus;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetPhoneHistory;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/phones")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class PhonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhonesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Phone>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConsumerPhoneHistoryAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var phoneNumbers = await _mediator.Send(new GetPhoneHistoryQuery(consumerId));
                
                return Ok(phoneNumbers);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PhonesController),
                          nameof(GetConsumerPhoneHistoryAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }


        [HttpPut("status")]
        [ProducesResponseType(typeof(Phone), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhoneStatusAsync(UpdatePhoneStatusCommand request)
        {
            try
            {
                var phoneUpdated = await _mediator.Send(request);
                
                return Ok(phoneUpdated);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PhonesController),
                          nameof(UpdatePhoneStatusAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

    }
}
