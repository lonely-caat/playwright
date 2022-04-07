using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Api.CustomerSummary.Application.Countries.Query.GetCountries;
using Zip.Api.CustomerSummary.Domain.Entities.ApiErrors;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Retrieve all countries
        /// </summary>
        /// <returns>A list of countries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var countries = await _mediator.Send(new GetCountriesQuery());
                
                if (countries.IsEmpty())
                {
                    return NotFound(new NotFoundError("Unable to find countries"));
                }
                
                return Ok(countries);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                          "{controller} :: {action} : {message}",
                          nameof(CountriesController),
                          nameof(GetAsync),
                          ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, new InternalServerError(ex.Message));
            }
        }
    }
}
