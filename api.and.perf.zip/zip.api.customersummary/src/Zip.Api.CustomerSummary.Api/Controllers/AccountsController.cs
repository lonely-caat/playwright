using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Api.Helpers;
using Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.UpdateInstallmentsEnabled;
using Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments;
using Zip.Api.CustomerSummary.Application.Accounts.Query.SearchAccounts;
using Zip.Api.CustomerSummary.Application.Payments.Command.CreateRepayment;
using Zip.Api.CustomerSummary.Application.Payments.Command.HoldPayment;
using Zip.Api.CustomerSummary.Application.Payments.Query.GetRepaymentSchedule;
using Zip.Api.CustomerSummary.Application.Transactions.Query.GetLmsTransactions;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Domain.Entities.Transactions;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<AccountListItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchByKeywordAsync([FromQuery]string keyword)
        {
            try
            {
                var results = await _mediator.Send(new SearchAccountsQuery(keyword));

                return Ok(results);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(SearchByKeywordAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("{accountId}/lmstransactions")]
        [ProducesResponseType(typeof(IEnumerable<LmsTransaction>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLmsTransactionsAsync(long accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest(new BadRequestError($"Account Id {accountId} is invalid"));
            }

            try
            {
                var transactions = await _mediator.Send(new GetLmsTransactionsQuery { AccountId = accountId });

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(GetLmsTransactionsAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("{accountId}/repaymentschedule")]
        public async Task<IActionResult> GetRepaymentScheduleAsync(long accountId)
        {
            if (accountId <= 0)
            {
                return BadRequest(new BadRequestError($"Account id {accountId} is invalid"));
            }

            try
            {
                var repaymentSchedule = await _mediator.Send(new GetRepaymentScheduleQuery(accountId));
                
                if (repaymentSchedule == null)
                {
                    return NoContent();
                }

                return Ok(repaymentSchedule);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(GetRepaymentScheduleAsync),
                          ex.Message);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("repayment")]
        public async Task<IActionResult> AddRepaymentAsync(CreateRepaymentCommand payload)
        {
            try
            {
                payload.ChangedBy = HttpContext.GetUserEmail();

                var repayment = await _mediator.Send(payload);
                
                return Ok(repayment);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(AddRepaymentAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("holdpayment")]
        public async Task<IActionResult> HoldPaymentAsync(HoldPaymentCommand payload)
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
                          nameof(AccountsController),
                          nameof(HoldPaymentAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpPost("lock")]
        public async Task<IActionResult> LockAccountAsync(LockAccountCommand payload)
        {
            if (payload.AccountId <= 0)
            {
                return BadRequest(new BadRequestError($"Account id {payload.AccountId} is invalid"));
            }

            try
            {
                payload.ChangedBy = HttpContext.GetUserEmail();
               
                await _mediator.Send(payload);
                
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(LockAccountAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        /// <summary>
        /// Call this to mark a consumer with an unauthorised claim
        /// </summary>
        /// <param name="payload"> </param>
        /// <returns></returns>
        [HttpPost("addAttributeAndLock")]
        public async Task<IActionResult> AddAttributeAndLockAccountAsync(AddAttributeAndLockAccountCommand payload)
        {
            try
            {
                payload.ChangedBy = HttpContext.GetUserEmail();
                await _mediator.Send(payload, CancellationToken.None);
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AccountsController),
                          nameof(AddAttributeAndLockAccountAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        /// <summary>
        /// Returns all orders with installments for a given account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{accountId}/installments")]
        [ProducesResponseType(typeof(OrdersResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status424FailedDependency)]
        public async Task<IActionResult> GetInstallments([FromRoute] GetAccountInstallmentsQuery request)
        {
            var response = await _mediator.Send(request);
            if (response?.Orders == null || !response.Orders.Any())
            {
                return NotFound();
            }

            return Ok(response.Orders);
        }
        
        /// <summary>
        /// Enables/Disables installments at the account level
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("configurations/installments")]
        [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateInstallmentsEnabled([FromBody] UpdateInstallmentsEnabledCommand request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
