using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/version")]
    [ApiController]
    [AllowAnonymous]
    [ExcludeFromCodeCoverage]
    public class VersionController: ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VersionController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public IActionResult GetCurrentVersion()
        {
            return Ok(_configuration["Version"]);
        }
    }
}
