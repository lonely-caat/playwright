using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Zip.Api.CustomerSummary.Application.Consumers.Command.SetConsumerAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetAttributes;
using Zip.Api.CustomerSummary.Application.Consumers.Query.GetConsumerAttributes;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Attribute = Zip.Api.CustomerSummary.Domain.Entities.Consumers.Attribute;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/internalconsumer")]
    [ApiController]
    [AllowAnonymous]
    public class InternalConsumerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InternalConsumerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("consumer/{consumerid}")]
        [ProducesResponseType(typeof(IEnumerable<ConsumerAttributeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConsumerAttributesAsync(long consumerId)
        {
            if (consumerId <= 0)
            {
                return BadRequest($"Consumer Id {consumerId} is invalid.");
            }

            try
            {
                var transactions = await _mediator.Send(new GetConsumerAttributesQuery { ConsumerId = consumerId });

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          $"{nameof(AttributesController)} :: {nameof(GetConsumerAttributesAsync)} : {ex.Message}",
                          consumerId);

                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPut("consumer")]
        public async Task<IActionResult> SetConsumerAttributesAsync(SetConsumerAttributesCommand payload)
        {
            try
            {
                var consumerAttributes = await _mediator.Send(new SetConsumerAttributesCommand{
                    ConsumerId = payload.ConsumerId,
                    Attributes = payload.Attributes
                });
                return Ok(consumerAttributes);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{nameof(AttributesController)} :: {nameof(SetConsumerAttributesAsync)} : {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpGet("attributes")]
        [ProducesResponseType(typeof(IEnumerable<Attribute>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttributesAsync()
        {
            try
            {
                var transactions = await _mediator.Send(new GetAttributesQuery());

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          $"{nameof(AttributesController)} :: {nameof(GetAttributesAsync)} : {ex.Message}");

                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}