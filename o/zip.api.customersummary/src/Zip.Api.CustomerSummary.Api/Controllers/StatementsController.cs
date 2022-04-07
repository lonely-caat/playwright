using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;
using Zip.Api.CustomerSummary.Application.Statements.Query.GetStatementDates;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.Statement;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/statements")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class StatementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatementsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Generate statement
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateStatementAsync(GenerateStatementCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(StatementsController),
                          nameof(GenerateStatementAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Calculate available statement date ranges
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet("availabledate")]
        [ProducesResponseType(typeof(IEnumerable<StatementDate>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableStatementDatesAsync(long accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest(new BadRequestError($"AccountId {accountId} is invalid"));
            }

            try
            {
                var statementDates = await _mediator.Send(new GetStatementDatesQuery(accountId));
                
                if (statementDates.IsEmpty())
                {
                    return NoContent();
                }
                
                return Ok(statementDates);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(StatementsController),
                          nameof(GetAvailableStatementDatesAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
