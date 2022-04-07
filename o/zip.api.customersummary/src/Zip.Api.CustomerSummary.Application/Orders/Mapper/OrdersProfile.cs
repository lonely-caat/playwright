using AutoMapper;
using Zip.Api.CustomerSummary.Application.Orders.Query.GetOrderSummary;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Requests;
using Zip.Api.CustomerSummary.Infrastructure.Services.MerchantDashboardService.Models.Responses;

namespace Zip.Api.CustomerSummary.Application.Orders.Mapper
{
    public class OrdersProfile : Profile
    {
        public OrdersProfile()
        {
            CreateMap<GetOrderSummaryQuery, OrderSummaryRequest>();

            CreateMap<OrderSummaryResponse, GetOrderSummaryResponse>();
        }
    }
}