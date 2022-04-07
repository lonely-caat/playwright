using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateBankPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Command.SetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetDefaultPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetLatestPaymentMethods;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethod;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetPaymentMethods;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/paymentmethods")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public PaymentMethodsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve consumer's payment methods
        /// </summary>
        /// <param name="consumerId">Consumer Id</param>
        /// <param name="state">State filter - ("Approved":"Removed":...)</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindAsync(long consumerId, string state)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var response = await _mediator.Send(new GetPaymentMethodsQuery(consumerId, true, state));

                if (response.IsEmpty())
                {
                    return NoContent();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentMethodsController),
                          nameof(FindAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Get latest payment methods
        /// </summary>
        /// <param name="consumerId">Consuer Id</param>
        /// <param name="state">The state - ("Approved" or "Removed")</param>
        /// <returns></returns>
        [HttpGet("latest")]
        [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> FindLatestOnlyAsync(long consumerId, string state = "Approved")
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var response = await _mediator.Send(new GetLatestPaymentMethodsQuery(consumerId, state));

                if (response.IsEmpty())
                {
                    return NoContent();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentMethodsController),
                          nameof(FindLatestOnlyAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Retrieve the payment method by id
        /// </summary>
        /// <param name="id">Payment method id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            if (id == default)
            {
                return BadRequest(new BadRequestError($"Payment method {id} is invalid"));
            }

            try
            {
                var paymentMethod = await _mediator.Send(new GetPaymentMethodQuery(id));
                
                if (paymentMethod == null)
                {
                    return NotFound(new NotFoundError($"Unable to find the latest payment method {id}"));
                }

                return Ok(paymentMethod);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentMethodsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Create a new payment method
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns></returns>
        [HttpPost("bank")]
        public async Task<IActionResult> CreateBankPaymentMethodAsync(CreateBankPaymentMethodCommand payload)
        {
            try
            {
                var response = await _mediator.Send(payload);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentMethodsController),
                          nameof(CreateBankPaymentMethodAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultPaymentMethodAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var defaultPaymentMethod = await _mediator.Send(new GetDefaultPaymentMethodQuery(consumerId));

                if (defaultPaymentMethod == null)
                {
                    return NotFound(new NotFoundError($"Unable to find default payment method for Consumer {consumerId}"));
                }

                return Ok(defaultPaymentMethod);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(PaymentMethodsController),
                          nameof(GetDefaultPaymentMethodAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("{PaymentMethodId}/default")]
        public async Task<IActionResult> SetDefaultPaymentMethodAsync(SetDefaultPaymentMethodCommand payload)
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
                          nameof(PaymentMethodsController),
                          nameof(SetDefaultPaymentMethodAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
