using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Api.Helpers;
using Zip.Api.CustomerSummary.Application.Beam.Command.CreateReconciliationReport;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetRewardActivity;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetCustomerDetails;
using Zip.Api.CustomerSummary.Domain.Common;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Beam;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions;
using Zip.Api.CustomerSummary.Domain.Common.Constants;
using Zip.Api.CustomerSummary.Application.Beam.Query.PollReconciliationReport;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using Zip.Api.CustomerSummary.Application.Beam.Query.GetTransactionRewardDetails;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/beam")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class BeamController : ControllerBase
    {
        private readonly ILogger<BeamController> _logger;
        private readonly IMediator _mediator;

        public BeamController(
            ILogger<BeamController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Given CustomerId (GUID), return Beam customer details
        /// </summary>
        /// <param name="customerId">The Guid of customer</param>
        /// <returns>Beam customer details</returns>
        [HttpGet("customers/{customerId}")]
        [ProducesResponseType(typeof(CustomerDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerDetailsAsync([FromRoute] Guid customerId)
        {
            try
            {
                var result = await _mediator.Send(new GetCustomerDetailsQuery(customerId));

                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (BeamApiException beamEx)
            {
                _logger.LogError(beamEx,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(GetCustomerDetailsAsync),
                          beamEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(GetCustomerDetailsAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Get a transaction's reward details
        /// </summary>
        /// <param name="request">CustomerId (GUID) and TransactionId (long)</param>
        /// <returns>Returns reward activity for a transaction if any</returns>
        [HttpGet("reward/transaction")]
        [ProducesResponseType(typeof(TransactionRewardDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionRewardDetailsAsync([FromQuery, Required] GetTransactionRewardDetailsQuery request)
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
            catch (BeamApiException beamEx)
            {
                _logger.LogError(beamEx,
                                 "{controller} :: {action} : {message}",
                                 nameof(BeamController),
                                 nameof(GetTransactionRewardDetailsAsync),
                                 beamEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                                 "{controller} :: {action} : {message}",
                                 nameof(BeamController),
                                 nameof(GetTransactionRewardDetailsAsync),
                                 ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Given customerId, pageNumber and pageSize, return a history of customer's reward activities
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="pageNumber">Pagination page to fetch</param>
        /// <param name="pageSize">Number of items to fetch within a single page</param>
        /// <param name="region">Region to query, only AU for now from Beam's side</param>
        /// <returns>Paginated Reward Activities of the customer</returns>
        [HttpGet("customers/{customerId}/reward-activity")]
        [ProducesResponseType(typeof(Pagination<RewardActivity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRewardActivityAsync([FromRoute, Required] Guid customerId, [FromQuery, Required] long pageNumber, [FromQuery] long pageSize = PaginationDefault.PageSize, [FromQuery] string region = Regions.Australia)
        {
            try
            {
                var result = await _mediator.Send(new GetRewardActivityQuery(customerId, pageNumber, pageSize, region));

                if (result == null || !result.Items.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (BeamApiException beamEx)
            {
                _logger.LogError(beamEx,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(CreateReconciliationReportAsync),
                          beamEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(GetCustomerDetailsAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Asynchronously creates reconciliation report for all beam users for a selected month
        /// </summary>
        /// <param name="request">Required Selected Date (Month and Year), RequestedBy populated in API from JWT token (for Audit), Region only AU for now on Beam's side</param>
        /// <returns>Guid as reference to the processing/processed report</returns>
        [HttpPost("reconciliation-report/create")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReconciliationReportAsync(CreateReconciliationReportCommand request)
        {
            try
            {
                request.RequestedBy = HttpContext.GetUserEmail();

                var result = await _mediator.Send(request);

                return Accepted(result);
            }
            catch (BeamApiException beamEx)
            {
                _logger.LogError(beamEx,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(CreateReconciliationReportAsync),
                          beamEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(CreateReconciliationReportAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Given a Guid uuid (Beam Report Id), make a proxy call to Beam API to fetch the report complete status and the URL to download it if complete
        /// </summary>
        /// <param name="uuid">Report Guid of an already created Beam reconciliation report</param>
        /// <returns>Uuid (Beam Report Id), Complete (true/false), URL to the report (has expiry)</returns>
        [HttpGet("reconciliation-report/poll")]
        [ProducesResponseType(typeof(PollReconciliationReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PollReconciliationReportAsync([FromQuery, Required] Guid uuid)
        {
            try
            {
                var requestedBy = HttpContext.GetUserEmail();

                var result = await _mediator.Send(new PollReconciliationReportQuery(uuid, requestedBy));

                return Ok(result);
            }
            catch (BeamApiException beamEx)
            {
                _logger.LogError(beamEx,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(PollReconciliationReportAsync),
                          beamEx.Message);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                          "{controller} :: {action} : {message}",
                          nameof(BeamController),
                          nameof(PollReconciliationReportAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}