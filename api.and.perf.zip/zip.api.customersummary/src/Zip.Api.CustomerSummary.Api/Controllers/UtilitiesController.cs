using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/utilities")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class UtilitiesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UtilitiesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("ip")]
        public IActionResult GetIpAddress()
        {
            return Ok(Request.HttpContext.Connection.RemoteIpAddress.ToString());
        }

        [HttpGet("datetime")]
        public IActionResult GetServerTime()
        {
            return Ok(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [HttpGet("config")]
        public IActionResult GetServerConfig()
        {
            var configPath = Path.Combine(_hostingEnvironment.ContentRootPath, $"appsettings.{_hostingEnvironment.EnvironmentName.ToLower()}.json");
            var configJson = System.IO.File.ReadAllText(configPath);
            
            return Ok(configJson);
        }
    }
}
