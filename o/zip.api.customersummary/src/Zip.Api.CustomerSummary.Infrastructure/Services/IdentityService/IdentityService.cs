using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserManagementProxy _userManagementProxy;

        private readonly IMemoryCache _cache;

        public IdentityService(IUserManagementProxy userManagementProxy, IMemoryCache memoryCache)
        {
            _userManagementProxy = userManagementProxy ?? throw new ArgumentNullException(nameof(userManagementProxy));
            _cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }
        
        public async Task<UserDetail> GetUserByEmailAsync(string email)
        {
            if (_cache.TryGetValue(email, out UserDetail userDetail))
            {
                return userDetail;
            }

            try
            {
                userDetail = await _userManagementProxy.GetUserByEmailAsync(email);

                if (userDetail != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(10) };
                    _cache.Set(email, userDetail, cacheEntryOptions);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"{nameof(IdentityService)} :: {nameof(GetUserByEmailAsync)} : {ex.Message}", email);
            }

            return userDetail;
        }
    }
}
