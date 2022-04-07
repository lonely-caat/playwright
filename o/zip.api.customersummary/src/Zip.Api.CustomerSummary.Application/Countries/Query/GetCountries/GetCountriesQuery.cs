using System.Collections.Generic;
using MediatR;
using Zip.Api.CustomerSummary.Domain.Entities.Countries;

namespace Zip.Api.CustomerSummary.Application.Countries.Query.GetCountries
{
    public class GetCountriesQuery : IRequest<IEnumerable<Country>>
    {
    }
}
