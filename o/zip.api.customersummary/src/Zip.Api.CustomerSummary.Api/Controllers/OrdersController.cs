using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Application.Orders.Command;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderInstallments;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(GetOrderSummaryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> GetOrderSummary([FromQuery] GetOrderSummaryQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("installments")]
        [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> GetOrderInstallments([FromQuery] GetOrderInstallmentsQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Cancels installments plan for a given order. Moves the amount owing to normal Zip repayment process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("installments/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> CancelOrderInstallments([FromBody] CancelOrderInstallmentsCommand request)
        {
            await _mediator.Send(request);

            return Ok();
        }
    }
}