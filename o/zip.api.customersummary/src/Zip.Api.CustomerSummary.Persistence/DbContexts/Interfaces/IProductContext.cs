using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Domain.Entities.Products;

namespace Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces
{
    public interface IProductContext
    {
        Task<IEnumerable<Product>> GetAsync();
        Task<Product> GetAsync(long id);
    }
}