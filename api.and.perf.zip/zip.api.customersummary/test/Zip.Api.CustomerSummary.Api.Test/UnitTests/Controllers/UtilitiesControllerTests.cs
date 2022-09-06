using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;
using Zip.Api.CustomerSummary.Api.Controllers;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Controllers
{
    public class UtilitiesControllerTests
    {
        private readonly Mock<IWebHostEnvironment> _hostingEnvironment;

        public UtilitiesControllerTests()
        {
            _hostingEnvironment = new Mock<IWebHostEnvironment>();
        }

        [Fact]
        private void Should_return_ip_address()
        {
            var controller = new UtilitiesController(_hostingEnvironment.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    { 
                        Connection = { RemoteIpAddress = IPAddress.Parse("128.0.0.1") }
                    }
                }
            };

            var actual = controller.GetIpAddress();

            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public void Should_return_server_time()
        {
            var controller = new UtilitiesController(_hostingEnvironment.Object);
            var ar = controller.GetServerTime();

            Assert.IsType<OkObjectResult>(ar);
        }
    }
}
