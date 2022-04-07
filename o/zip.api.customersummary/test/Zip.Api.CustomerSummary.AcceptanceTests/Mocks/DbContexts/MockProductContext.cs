using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Products;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Mocks.DbContexts
{
    public class MockProductContext : IProductContext
    {
        private readonly List<Product> _products;

        public MockProductContext()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Classification = ProductClassification.zipPay,
                    CountryId = "AU",
                    Status = 1
                },
                new Product
                {
                    Classification = ProductClassification.zipPay,
                    CountryId = "NZ",
                    Status = 1
                },
                new Product
                {
                    Classification = ProductClassification.zipMoney,
                    CountryId = "AU",
                    Status = 1
                },
                new Product
                {
                    Classification = ProductClassification.zipMoney,
                    CountryId = "NZ",
                    Status =1
                }
            };
        }

        public Task<IEnumerable<Product>> GetAsync()
        {
            return Task.FromResult<IEnumerable<Product>>(_products);
        }

        public Task<Product> GetAsync(long id)
        {
            return Task.FromResult(_products.FirstOrDefault());
        }
    }
}
