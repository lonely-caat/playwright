using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public class Phone
    {
        public long Id { get; set; }

        public string PhoneNumber { get; set; }

        public bool Preferred { get; set; }

        public DateTime TimeStamp { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PhoneType PhoneType { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }
    }
}
