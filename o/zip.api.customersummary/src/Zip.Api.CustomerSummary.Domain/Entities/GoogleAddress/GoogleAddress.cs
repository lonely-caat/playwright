namespace Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress
{
    public class GoogleAddress
    {
        public string UnitNumber { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string CountryCode { get; set; }

        public string FormattedAddress { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
