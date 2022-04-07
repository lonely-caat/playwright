using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayNow;
using Zip.Api.CustomerSummary.Application.Payments.Command.PayOrder;
using Zip.Api.CustomerSummary.Application.Payments.Command.Refund;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayment;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPayments;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetUpcomingInstallments;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.CoreGraphService.Models.Response.PayOrder;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get desired payment by id
        /// </summary>
        /// <param name="id">payment id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new BadRequestError($"Payment {id} is invalid"));
            }

            try
            {
                var payment = await _mediator.Send(new GetPaymentQuery(id));

                if (payment == null)
                {
                    return NotFound(new NotFoundError($"Unable to find Payment {id}"));
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Find payments via criteria
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <param name="paymentBatchId">batch id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindAsync(
            long accountId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Guid? paymentBatchId = null)
        {
            if (accountId <= 0)
            {
                return BadRequest(new BadRequestError($"AccountId {accountId} is invalid"));
            }

            try
            {
                var payments = await _mediator.Send(new GetPaymentsQuery(accountId, fromDate, toDate, paymentBatchId));
                
                if (payments.IsEmpty())
                {
                    return NoContent();
                }
                
                return Ok(payments);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentsController),
                          nameof(FindAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("{id}/refund")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefundAsync(Guid id)
        {
            if (id == default)
            {
                return BadRequest(new BadRequestError($"Payment {id} is invalid"));
            }

            try
            {
                var response = await _mediator.Send(new RefundCommand(id));
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentsController),
                          nameof(RefundAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("paynow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PayNowAsync(PayNowCommand request)
        {
            try
            {
                var response = await _mediator.Send(request);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentsController),
                          nameof(PayNowAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Get a list of upcoming installments up to the given toDate param in the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("repayment-installments")]
        [ProducesResponseType(typeof(GetUpcomingInstallmentsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> GetUpcomingInstallmentsAsync([FromQuery] GetUpcomingInstallmentsQuery request)
        {
            var response = await _mediator.Send(request);

            if (response?.Installments == null || !response.Installments.Any())
            {
                return NoContent();
            }

            return Ok(response);
        }
        
        /// <summary>
        /// Make payment against an order. Note: at the moment of creating this endpoint, it's only compatible for orders with installments
        /// The underlying service (Core Product GraphQL) makes an additional payment against the specified Order
        /// </summary>
        /// <param name="request">AccountId, CustomerId, OrderId and Amount</param>
        /// <returns>code, success (boolean) and message (if any)</returns>
        [HttpPost("pay-order")]
        [ProducesResponseType(typeof(PayOrderInnerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> PayOrderAsync([FromBody] PayOrderCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

    }
}
