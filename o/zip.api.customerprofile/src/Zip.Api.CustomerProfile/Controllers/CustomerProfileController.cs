using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zip.Api.CustomerProfile.Interfaces;
using Zip.CustomerProfile.Contracts;

namespace Zip.Api.CustomerProfile.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerProfileController : Controller
    {
        private readonly ICustomerProfileService _customerProfileService;

        public CustomerProfileController(ICustomerProfileService customerProfileService)
        {
            _customerProfileService = customerProfileService;
        }

        [HttpGet]
        [Route("search/{keyword}")]
        [Obsolete("Please use GraphQL")]
        public async Task<IEnumerable<Customer>> Search(string keyword)
        {
            return await _customerProfileService.GetCustomersByKeyword(keyword, Guid.NewGuid().ToString());
        }
    }
}