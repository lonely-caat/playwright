using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.Mocks.Services
{
    public class MockZipUrlShorteningService : IZipUrlShorteningService
    {
        public Task<string> GetZipShortenedUrlAsync(string longUrl)
        {
            return Task.FromResult("test");
        }
    }
}
