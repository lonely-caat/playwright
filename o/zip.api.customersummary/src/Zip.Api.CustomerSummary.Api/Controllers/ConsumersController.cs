using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Api.Helpers;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInfo.Models;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetCloseAccountCreditProfile.Models;
using Zip.Api.CustomerSummary.Application.Consumers.Command.RefreshCache;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateLoginStatus;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.V1;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumer.Vcn;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetLoginStatus;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;
using Zip.Api.CustomerSummary.Infrastructure.Services.CustomerCoreService.Models;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/consumers")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class ConsumersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConsumersController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve the desired consumer by id.
        /// </summary>
        /// <param name="consumerId">The id of consumer.</param>
        /// <returns></returns>
        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(Consumer), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var result = await _mediator.Send(new GetConsumerQuery(consumerId));

                if (result == null)
                {
                    return NotFound(new NotFoundError($"Unable to find Consumer {consumerId}"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Retrieve the desired consumer by customerId and product.
        /// </summary>
        /// <param name="request">The customerId and product of consumer.</param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(Consumer), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConsumerForVcnAsync([FromQuery] GetConsumerForVcnQuery request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == null)
                {
                    return NotFound(new NotFoundError($"Unable to find Consumer with customerId: {request.CustomerId}"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(GetConsumerForVcnAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Update desired consumer.
        /// </summary>
        /// <param name="payload">
        /// <see cref="UpdateConsumerCommand"/>
        /// </param>
        /// <returns>Result</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateConsumerCommand payload)
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
                          nameof(ConsumersController),
                          nameof(UpdateAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        /// <summary>
        /// Get desired account info.
        /// </summary>
        /// <param name="consumerId">The id of consumer.</param>
        /// <returns>Account Info</returns>
        [HttpGet("{consumerId}/account")]
        [ProducesResponseType(typeof(GetAccountInfoQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountInfoAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var queryResult = await _mediator.Send(new GetAccountInfoQuery(consumerId));

                if (queryResult == null)
                {
                    return NotFound(new NotFoundError($"Unable to find info for consumer {consumerId}"));
                }

                return Ok(queryResult);
            }
            catch (ArgumentException aex)
            {
                return BadRequest(new BadRequestError(aex.Message));
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(GetAccountInfoAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// This endpoint is used to fetch the required data for closing account.
        /// </summary>
        /// <param name="consumerId">Consumer Id</param>
        /// <returns></returns>
        [HttpGet("{consumerId}/closeaccount")]
        [ProducesResponseType(typeof(GetCloseAccountCreditProfileQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCloseAccountCreditProfileAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var cp = await _mediator.Send(new GetCloseAccountCreditProfileQuery(consumerId));
                
                return Ok(cp);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(GetCloseAccountCreditProfileAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Call this to close account
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("{consumerId}/closeaccount")]
        public async Task<IActionResult> CloseAccountAsync(CloseAccountCommand payload)
        {
            try
            {
                payload.ChangedBy = HttpContext.GetUserEmail();
                
                await _mediator.Send(payload);
                
                return Ok();
            }
            catch (CloseAccountUnprocessableException uauex)
            {
                Log.Error(uauex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(CloseAccountAsync),
                          uauex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(uauex.Message));
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ConsumersController),
                          nameof(CloseAccountAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        [HttpPut("{consumerId}/cache-refresh")]
        public async Task<ActionResult> CacheRefresh(RefreshCacheCommand payload)
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
                    nameof(ConsumersController),
                    nameof(CacheRefresh),
                    ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        [HttpGet("login-status")]
        [ProducesResponseType(typeof(LoginStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailedDependencyError), StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLoginStatusAsync([FromQuery] GetLoginStatusQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
        [HttpPost("login-status")]
        [ProducesResponseType(typeof(UpdateLoginStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnprocessableEntityError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(FailedDependencyError), StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(typeof(InternalServerError), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateLoginStatusAsync([FromBody] UpdateLoginStatusCommand command)
        {
            command.ChangedBy = HttpContext.GetUserEmail();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}