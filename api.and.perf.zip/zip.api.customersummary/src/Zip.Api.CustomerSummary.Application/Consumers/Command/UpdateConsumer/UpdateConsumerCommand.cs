using System;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;

namespace Zip.Api.CustomerSummary.Application.Consumers.Command.UpdateConsumer
{
    public class UpdateConsumerCommand : IRequest
    {
        public long ConsumerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryName { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public Address Address { get; set; }
    }
}
