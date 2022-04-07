using MediatR;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;

namespace Zip.Api.CustomerSummary.Application.Accounts.Query.GetAccountInstallments
{
    public class GetAccountInstallmentsQuery: IRequest<OrdersResponse>
    {
        [FromRoute]
        public long AccountId { get; set; }
    }}