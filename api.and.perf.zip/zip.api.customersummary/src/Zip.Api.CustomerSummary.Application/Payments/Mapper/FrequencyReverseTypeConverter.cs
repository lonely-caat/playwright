using System;
using AutoMapper;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;

namespace Zip.Api.CustomerSummary.Application.Payments.Mapper
{
    public class FrequencyReverseTypeConverter : ITypeConverter<Frequency, Domain.Entities.Tango.Frequency>
    {
        public Domain.Entities.Tango.Frequency Convert(Frequency source, Domain.Entities.Tango.Frequency destination, ResolutionContext context)
        {
            switch (source)
            {
                case Frequency.Fortnightly:
                    return Domain.Entities.Tango.Frequency.Fortnightly;
                case Frequency.Monthly:
                    return Domain.Entities.Tango.Frequency.Monthly;
                case Frequency.Weekly:
                    return Domain.Entities.Tango.Frequency.Weekly;
                case Frequency.Once:
                    return Domain.Entities.Tango.Frequency.Once;
                default:
                    throw new NotSupportedException($"Cannot convert {source}.");
            }
        }
    }
}