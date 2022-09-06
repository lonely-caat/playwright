using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Zip.Api.CustomerSummary.Api.Attributes;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;
using Zip.Api.CustomerSummary.Infrastructure.Services.GoogleService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/address")]
    [ApiController]
    [ApiVersion("1.0")]
    [AuthorizeRole(AppRole.Admin, AppRole.CSPAdmin, AppRole.CSPDefault, AppRole.CollectionAdmin, AppRole.CollectionLevel1, AppRole.CollectionDefault)]
    public class AddressController : ControllerBase
    {
        private readonly IAddressSearch _addressSearch;

        public AddressController(IAddressSearch addressSearch)
        {
            _addressSearch = addressSearch ?? throw new ArgumentNullException(nameof(addressSearch));
        }

        /// <summary>
        /// Search addresses by input.
        /// </summary>
        /// <param name="input">Keyword</param>
        /// <param name="countryCode">The country code ('au':'nz').</param>
        /// <returns>A list of address predictions.</returns>
        [HttpGet("search/{input}")]
        [ProducesResponseType(typeof(IEnumerable<Prediction>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchAsync(string input, string countryCode)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest(new BadRequestError("Input is empty"));
            }

            try
            {
                var address = await _addressSearch.SearchAsync(countryCode, input);

                if (address.IsEmpty())
                {
                    return NoContent();
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AddressController),
                          nameof(SearchAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
        
        /// <summary>
        /// Address validation.
        /// </summary>
        /// <param name="input">The full address.</param>
        /// <returns>Status</returns>
        [HttpGet("verify/{input}")]
        [ProducesResponseType(typeof(GoogleAddress), StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest(new BadRequestError("Input is empty"));
            }
            
            try
            {
                var address = await _addressSearch.FindAndVerifyAsync(input);

                if (address == null)
                {
                    return NotFound(new NotFoundError($"Unable to find address by {input}"));
                }

                return Ok(address);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(AddressController),
                          nameof(VerifyAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}