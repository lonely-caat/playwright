using AutoMapper;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Models;
using Zip.Services.Accounts.Contract.Order;
using OrderDetailResponse = Zip.Services.Accounts.Contract.Order.OrderDetailResponse;
using OrdersResponse = Zip.Services.Accounts.Contract.Order.OrdersResponse;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountsService.Mapper
{
    public class AccountsServiceProfile : Profile
    {
        public AccountsServiceProfile()
        {
            CreateMap<OrdersResponse, Models.OrdersResponse>();
            
            CreateMap<OrderDetailResponse, Models.OrderDetailResponse>();
            
            CreateMap<Installment, InstallmentResponse>();
        }
    }
}
