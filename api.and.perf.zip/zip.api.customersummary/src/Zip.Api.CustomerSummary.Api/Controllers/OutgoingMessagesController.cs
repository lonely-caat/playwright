using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Serilog;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPaidOutAndClosedEmail;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendPayNowLink;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/outgoingmessages")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class OutgoingMessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OutgoingMessagesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("paynowlink/sms")]
        public async Task<IActionResult> SendPayNowLinkAsync(SendPayNowLinkCommand payload)
        {
            try
            {
                await _mediator.Send(payload);

                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(OutgoingMessagesController),
                          nameof(SendPayNowLinkAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("send/paidoutclose-letter/{consumerId}")]
        public async Task<IActionResult> SendPaidOutAndCloseEmailAsync(long consumerId)
        {
            try
            {
                if(consumerId < 0)
                {
                    return BadRequest();
                }

                var result = await _mediator.Send(new SendPaidOutAndClosedEmailCommand
                {
                    ConsumerId = consumerId,
                });

                if (!result)
                {
                    return NoContent();
                }
                
                return Ok();
            }
            catch (ValidationException vex)
            {
                return BadRequest(new BadRequestError(JsonConvert.SerializeObject(vex)));
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(OutgoingMessagesController),
                          nameof(SendPaidOutAndCloseEmailAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
