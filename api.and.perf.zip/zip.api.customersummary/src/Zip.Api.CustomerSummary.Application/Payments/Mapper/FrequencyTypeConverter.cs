using System;
using AutoMapper;
using Zip.Api.CustomerSummary.Domain.Entities.Tango;

namespace Zip.Api.CustomerSummary.Application.Payments.Mapper
{
    public class FrequencyTypeConverter : ITypeConverter<Domain.Entities.Tango.Frequency, Frequency>
    {
        public Frequency Convert(Domain.Entities.Tango.Frequency source, Frequency destination, ResolutionContext context)
        {
            switch (source)
            {
                case Domain.Entities.Tango.Frequency.Fortnightly:
                    return Frequency.Fortnightly;
                case Domain.Entities.Tango.Frequency.Monthly:
                    return Frequency.Monthly;
                case Domain.Entities.Tango.Frequency.Weekly:
                    return Frequency.Weekly;
                case Domain.Entities.Tango.Frequency.Once:
                    return Frequency.Once;
                default:
                    throw new NotSupportedException($"Cannot convert {source}.");
            }
        }
    }
}