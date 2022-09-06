using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/values")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IIdentityService _identityService;

        private readonly IUserManagementProxy _userManagementProxy;

        public ValuesController(
            IConfiguration configuration,
            IIdentityService identityService,
            IUserManagementProxy userManagementProxy)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userManagementProxy = userManagementProxy ?? throw new ArgumentNullException(nameof(userManagementProxy));
        }

        [HttpGet("umurl")]
        [AllowAnonymous]
        public IActionResult GetUserManagementClusterInternalUrl()
        {
            var clusterInternalBaseUrl = Environment.ExpandEnvironmentVariables($"%{_configuration["IdentityServiceProxy:ClusterInternalUrlVar"]}%");
            var port = _configuration["IdentityServiceProxy:ClusterInternalUrlPort"];

            return Ok(new Uri($"http://{clusterInternalBaseUrl}:{port}"));
        }

        [HttpGet("umhealth")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserManagementHealthAsync()
        {
            try
            {
                var response = await _userManagementProxy.HealthCheck();

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ValuesController),
                          nameof(GetUserManagementHealthAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("me")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMyUserDetailAsync([FromQuery]string email="")
        {
            try
            {
                var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var userDetail = await _identityService.GetUserByEmailAsync(userEmail ?? email);

                return Ok(userDetail);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ValuesController),
                          nameof(GetMyUserDetailAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }

        [HttpGet("oneloginclientid")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOneLoginClientIdAsync()
        {
            try
            {
                var rv = await Task.Run(() => {
                    var clientId = _configuration["OneLogin:ClientId"];
                    return new
                    {
                        clientId
                    };
                });

                return Ok(rv);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(ValuesController),
                          nameof(GetOneLoginClientIdAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}