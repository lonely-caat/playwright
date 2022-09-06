using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Mfa.Query;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.Mfa;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/mfa")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class MfaController : ControllerBase
    {
        private readonly ILogger<MfaController> _logger;
        private readonly IMediator _mediator;

        public MfaController(
            ILogger<MfaController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Given consumerId (long), return MFA Sms data
        /// </summary>
        /// <param name="consumerId">The consumerId of the customer</param>
        /// <returns>MFA Sms data</returns>
        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(MfaSmsDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMfaSmsDataAsync([FromRoute] long consumerId)
        {
            try
            {
                var result = await _mediator.Send(new GetMfaSmsDataQuery(consumerId));

                if (result == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                return Ok(result.Data);
            }
            catch (MfaApiException mfaEx)
            {
                _logger.LogError(mfaEx,
                    "{controller} :: {action} : {message}",
                    nameof(MfaController),
                    nameof(GetMfaSmsDataAsync),
                    mfaEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "{controller} :: {action} : {message}",
                    nameof(MfaController),
                    nameof(GetMfaSmsDataAsync),
                    ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}