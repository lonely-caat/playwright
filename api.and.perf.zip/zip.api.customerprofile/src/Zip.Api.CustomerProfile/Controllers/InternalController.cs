using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerProfile.Interfaces;

namespace Zip.Api.CustomerProfile.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternalController : Controller
    {
        private readonly ICustomerProfileBackgroundService _customerProfileBackgroundService;

        public InternalController(ICustomerProfileBackgroundService customerProfileBackgroundService)
        {
            _customerProfileBackgroundService = customerProfileBackgroundService;
        }

        [HttpDelete]
        [Route("customer/{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id is empty");
            }

            await _customerProfileBackgroundService.DeleteCustomerById(id, Guid.NewGuid().ToString());

            return Accepted();
        }
        
        [HttpPut]
        [Route("customer/{id}")]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id is empty");
            }

            await _customerProfileBackgroundService.UpdateCustomerById(id, Guid.NewGuid().ToString());

            return Accepted();
        }
    }
}
