using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Zip.Api.CustomerSummary.Infrastructure.Configuration.OutgoingMessages;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService.Models;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.ZipUrlShorteningService
{
    public class ZipUrlShorteningService : IZipUrlShorteningService
    {
        private readonly IOutgoingMessagesConfig _outgoingMessagesConfig;
        
        public ZipUrlShorteningService(IOutgoingMessagesConfig outgoingMessagesConfig)
        {
            _outgoingMessagesConfig = outgoingMessagesConfig ?? throw new ArgumentNullException(nameof(outgoingMessagesConfig));
        }

        public async Task<string> GetZipShortenedUrlAsync(string longUrl)
        {
            var jumpPage = _outgoingMessagesConfig.ZipUrlShortenerJumpPage;

            var body = JsonConvert.SerializeObject(new ZipUrlShorteningRequest()
            {
                Action = "get_id",
                Params = new ZipUrlShortenParams()
                {
                    Url = longUrl,
                    Og = new ZipUrlShortenOg()
                    {
                        Title = true.ToString(),
                        Description = _outgoingMessagesConfig.ZipUrlShortenerDescription,
                        SiteName = true.ToString()
                    }
                }
            }, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync(_outgoingMessagesConfig.ZipUrlShortenerApi, content);
                    var payload = await result.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ZipUrlShorteningResponse>(payload);
                    return $"{jumpPage}/{response.Id}";
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"{nameof(ZipUrlShorteningService)}.{nameof(GetZipShortenedUrlAsync)} :: Error while shortening url: {longUrl}");
                    throw;
                }
            }
        }
    }
}
