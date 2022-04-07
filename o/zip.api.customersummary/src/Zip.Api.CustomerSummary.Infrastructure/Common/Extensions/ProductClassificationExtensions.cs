using System;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Infrastructure.Common.Exceptions.Contract;

namespace Zip.Api.CustomerSummary.Infrastructure.Common.Extensions
{
    public static class ProductClassificationExtensions
    {
        public static string DisplayName(this ProductClassification productClassification)
        {
            switch (productClassification)
            {
                case ProductClassification.zipMoney:
                    return "Zip Money";
                
                case ProductClassification.zipPay:
                    return "Zip Pay";
                
                case ProductClassification.zipBiz:
                    return "Zip Biz";
                
                default:
                    throw new InvalidProductCodeException(
                            Enum.GetName(typeof(ProductClassification), productClassification));
            }
        }
    }
}