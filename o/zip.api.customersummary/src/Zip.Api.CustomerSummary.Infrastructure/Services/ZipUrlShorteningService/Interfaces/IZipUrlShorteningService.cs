using System.Threading.Tasks;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces
{
    public interface IZipUrlShorteningService
    {
        Task<string> GetZipShortenedUrlAsync(string longUrl);
    }
}
