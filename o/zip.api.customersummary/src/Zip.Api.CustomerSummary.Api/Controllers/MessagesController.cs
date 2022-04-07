using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Zip.Api.CustomerSummary.Application.Communications.Query.GetEmailsSent;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Communications;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/message")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public MessagesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("emails-sent")]
        [ProducesResponseType(typeof(IEnumerable<EmailSent>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmailsSentAsync(GetEmailsSentQuery request)
        {
            try
            {    
                var response = await _mediator.Send(request);
                
                if(response == null)
                {
                    return NoContent();
                }
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(MessagesController),
                          nameof(GetEmailsSentAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}