using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Infrastructure.Configuration;
using Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.AccountSearchService
{
    public class AccountSearchServiceClient : IAccountSearchServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly AccountSearchSettings _settings;

        public AccountSearchServiceClient(HttpClient httpClient, IOptions<AccountSearchSettings> settings)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public async Task<IEnumerable<AccountListItem>> SearchAccountsAsync(CustomerSearchType searchType, string searchValue, int skip, int take)
        {
            try
            {
                var queryUrl = $"{_settings.AccountSearchUrl}?query={searchValue}&type={searchType}&skip={skip}&take={take}";


                var response = await _httpClient.GetAsync(queryUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content= await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<AccountListItem>>(content);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AccountSearchService.SearchAccounts");
                return new List<AccountListItem>();
            }

            return new List<AccountListItem>();
        }

        public async Task GetStatusAsync()
        {
            var response = await _httpClient.GetAsync("/accountsearch/api/v1/status");
            response.EnsureSuccessStatusCode();
        }
    }
}
