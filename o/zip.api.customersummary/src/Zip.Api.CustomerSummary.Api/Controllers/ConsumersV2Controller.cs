using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V2;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/v{v:apiVersion}/consumers")]
    [ApiController]
    [ApiVersion("2.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class ConsumersV2Controller : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsumersV2Controller(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve the desired consumer by id.
        /// </summary>
        /// <param name="request">GetConsumerV2Query request that has ConsumerId</param>
        /// <returns></returns>
        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(Consumer), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetConsumerQueryV2 request)
        {
            try
            {
                var result = await _mediator.Send(request);

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
                          nameof(ConsumersV2Controller),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}