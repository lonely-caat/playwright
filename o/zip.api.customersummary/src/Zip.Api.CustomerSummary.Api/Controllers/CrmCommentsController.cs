using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Api.Helpers;
using Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment;
using Zip.Api.CustomerSummary.Application.Comments.Query.GetCrmComments;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/crmcomments")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault)]
    public class CrmCommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CrmCommentsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet("{consumerId}")]
        [ProducesResponseType(typeof(IEnumerable<CommentDto>), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAsync(long consumerId, [FromQuery] long pageIndex = 1, [FromQuery] long pageSize = 100)
        {
            if (consumerId <= 0)
            {
                return BadRequest(new BadRequestError($"ConsumerId {consumerId} is invalid"));
            }

            try
            {
                var result = await _mediator.Send(new GetCrmCommentsQuery(consumerId, pageIndex, pageSize));

                if (result == null)
                {
                    return NotFound(new NotFoundError($"Unable to find CRM comments for Consumer {consumerId}"));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CrmCommentsController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommentDto), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAsync(CreateCrmCommentCommand payload)
        {
            try
            {
                payload.CommentBy = HttpContext.GetUserEmail();
                
                await _mediator.Send(payload);

                return await GetAsync(payload.ReferenceId);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CrmCommentsController),
                          nameof(CreateAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
