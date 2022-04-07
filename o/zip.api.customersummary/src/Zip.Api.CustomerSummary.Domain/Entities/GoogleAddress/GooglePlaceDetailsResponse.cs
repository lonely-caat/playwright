using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress
{
    public class GooglePlaceDetailsResponse
    {
        public Result Result { get; set; }

        public string Status { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> Address_Components { get; set; }

        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public decimal Lat { get; set; }

        public decimal Lng { get; set; }
    }

    public class AddressComponent
    {
        public List<string> Types { get; set; }

        public string Long_Name { get; set; }

        public string Short_Name { get; set; }
    }
}
