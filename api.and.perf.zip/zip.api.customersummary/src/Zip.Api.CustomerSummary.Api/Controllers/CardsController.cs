using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Serilog;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.BlockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.CloseCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.TerminateDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.UnblockDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Command.VerifyDigitalWalletToken;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCards;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCardTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.VcnCard;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/cards")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet()]
        [ProducesResponseType(typeof(Card), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetCardQuery request)
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
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("{cardId}/transactions")]
        [ProducesResponseType(typeof(List<CardTransaction>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCardTransactionsAsync([FromQuery] GetCardTransactionsQuery request)
        {
            try
            {
                var result = await _mediator.Send(request);

                if (result == null || !result.Any())
                {
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "{controller} :: {action} : {message}",
                    nameof(CardsController),
                    nameof(GetCardTransactionsAsync),
                    ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(RootCards), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCardsAsync([FromQuery] GetCardsQuery request)
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
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(GetCardsAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPut("{cardId}/block")]
        public async Task<IActionResult> BlockCardAsync([FromQuery] BlockCardCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(BlockCardAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPut("{cardId}/unblock")]
        public async Task<IActionResult> UnblockCardAsync([FromQuery] UnblockCardCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(UnblockCardAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPut("{cardId}/close")]
        [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin)]
        public async Task<IActionResult> CloseCardAsync([FromQuery] CloseCardCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(CloseCardAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("digitalwallettokens/{digitalWalletToken}/block")]
        public async Task<IActionResult> BlockDigitalWalletTokenAsync([FromQuery] BlockDigitalWalletTokenCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(BlockDigitalWalletTokenAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("digitalwallettokens/{digitalWalletToken}/unblock")]
        public async Task<IActionResult> UnblockDigitalWalletTokenAsync([FromQuery] UnblockDigitalWalletTokenCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(UnblockDigitalWalletTokenAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("digitalwallettokens/{digitalWalletToken}/terminate")]
        [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin)]
        public async Task<IActionResult> TerminateDigitalWalletTokenAsync([FromQuery] TerminateDigitalWalletTokenCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(TerminateDigitalWalletTokenAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("digitalwallettokens/{digitalWalletToken}/verify")]
        public async Task<IActionResult> VerifyDigitalWalletTokenAsync([FromQuery] VerifyDigitalWalletTokenCommand request)
        {
            try
            {
                await _mediator.Send(request);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CardsController),
                          nameof(TerminateDigitalWalletTokenAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
