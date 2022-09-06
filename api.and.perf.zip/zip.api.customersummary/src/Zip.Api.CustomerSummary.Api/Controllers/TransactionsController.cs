using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetOrderActivities;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetTransactionHistory;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetVcnTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto.OrderActivity;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve consumer's transactions
        /// </summary>
        /// <param name="consumerId">Consumer Id (required)</param>
        /// <param name="startDate">Start date (optional)</param>
        /// <param name="endDate">End date (optional)</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindTransactionHistoryAsync(long consumerId, DateTime? startDate, DateTime? endDate)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var transactions = await _mediator.Send(new GetTransactionHistoryQuery(consumerId, startDate, endDate));
                
                if (transactions.IsEmpty())
                {
                    return NoContent();
                }
                
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(TransactionsController),
                          nameof(FindTransactionHistoryAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Retrieve consumer's order activity
        /// </summary>
        /// <param name="consumerId">(required)</param>
        /// <param name="fromDate">(optional) Period start. Defaults to 2 months ago</param>
        /// <param name="toDate">(optional) Period end. Defaults to tomorrow</param>
        /// <param name="showAll">(optional) Whether to include parent orders or not. Defaults to false</param>
        /// <returns></returns>
        [HttpGet("orderactivity")]
        [ProducesResponseType(typeof(IEnumerable<OrderActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetOrderActivityAsync(long consumerId, DateTime? fromDate, DateTime? toDate, bool showAll = false)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var orderActivities = await _mediator.Send(new GetOrderActivitiesQuery(consumerId, fromDate, toDate, showAll));
                if (orderActivities.IsEmpty())
                {
                    return NoContent();
                }
                
                return Ok(orderActivities);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(TransactionsController),
                          nameof(GetOrderActivityAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Retrieve VCN transactions
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("vcn-transactions")]
        [ProducesResponseType(typeof(CardTransaction), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetVcnTransactionsAsync([FromQuery] GetVcnTransactionsQuery request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == null || result.IsEmpty())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(TransactionsController),
                          nameof(GetVcnTransactionsAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
