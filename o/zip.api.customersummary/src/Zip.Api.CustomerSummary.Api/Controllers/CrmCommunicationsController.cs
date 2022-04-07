using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Communications.Command.InsertCrmCommunication;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetCrmCommunications;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.MessageLog;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/crmcommunications")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionDefault, AppRole.CollectionLevel1, AppRole.CollectionAdmin)]
    public class CrmCommunicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CrmCommunicationsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(IEnumerable<MessageLogDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var result = await _mediator.Send(new GetCrmCommunicationsQuery
                {
                    MessageLogCategory = MessageLogCategory.Consumer,
                    ReferenceId = consumerId
                });

                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CrmCommunicationsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<MessageLogDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> InsertCommunicationCommentAsync(InsertCrmCommunicationCommand crmCommunicationCommand)
        {
            try
            {
                var result = await _mediator.Send(crmCommunicationCommand);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CrmCommunicationsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }

        }
    }
}
