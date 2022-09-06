using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendResetPasswordEmailNew;
using Zip.Api.CustomerSummary.Application.Communications.Command.SendSmsCode;
using Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateContact;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetContact;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve the contacts by consumer Id.
        /// </summary>
        /// <param name="consumerId">The id of the consumer.</param>
        /// <returns>A list of contacts.</returns>
        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var result = await _mediator.Send(new GetContactQuery(consumerId));
                
                if (result == null)
                {
                    return NotFound(new NotFoundError($"Unable to find contacts for Consumer {consumerId}"));
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ContactsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Update the contact.
        /// </summary>
        /// <param name="payload">Check<see cref="UpdateContactCommand"/>.</param>
        /// <returns>Result</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(UpdateContactCommand payload)
        {
            try
            {
                var result = await _mediator.Send(payload);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ContactsController),
                          nameof(UpdateAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Generate a reset password email for the consumer.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("password/reset")]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPasswordAsync(SendResetPasswordEmailNewCommand payload)
        {
            var result = await _mediator.Send(payload);
            return Ok(result);
        }

        /// <summary>
        /// Send SMS Code to customer's phone 
        /// </summary>
        /// <param name="consumerId"></param>
        /// <returns></returns>
        [HttpPost("smscode")]
        public async Task<IActionResult> SendSmsCodeAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var smsCode = await _mediator.Send(new SendSmsCodeCommand(consumerId));
                
                return Ok(new { smsCode });
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ContactsController),
                          nameof(SendSmsCodeAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
