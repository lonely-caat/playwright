using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/internalaccount")]
    [ApiController]
    [AllowAnonymous]
    public class InternalAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InternalAccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("lock")]
        public async Task<IActionResult> LockAccountAsync(LockAccountCommand payload)
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
                          nameof(InternalAccountController),
                          nameof(LockAccountAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}