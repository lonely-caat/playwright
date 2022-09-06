namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models
{
    public class VerifyAddressResponseResult
    {
        public string DPID { get; set; }

        public string BuildingName { get; set; }

        public string AddressLine { get; set; }

        public string Locality { get; set; }

        public string State { get; set; }

        public string Postcode { get; set; }

        public string AddressBlockLine1 { get; set; }

        public string AddressBlockLine2 { get; set; }

        public string AddressBlockLine3 { get; set; }

        public string AddressBlockLine4 { get; set; }

        public string AddressBlockLine5 { get; set; }

        public string AddressBlockLine6 { get; set; }

        public string AddressBlockLine7 { get; set; }

        public string MatchType { get; set; }

        public string MatchTypeDescription { get; set; }

        public string FieldChanges { get; set; }

        public string AustraliaPostBarcode { get; set; }

        public string AustraliaPostBarcodeSortPlan { get; set; }

        public string UnitType { get; set; }

        public string UnitNumber { get; set; }

        public string LevelType { get; set; }

        public string LevelNumber { get; set; }

        public string LotNumber { get; set; }

        public string StreetNumber1 { get; set; }

        public string StreetNumberSuffix1 { get; set; }

        public string StreetNumber2 { get; set; }

        public string StreetNumberSuffix2 { get; set; }

        public string PostBoxNumber { get; set; }

        public string PostBoxNumberPrefix { get; set; }

        public string PostBoxNumberSuffix { get; set; }

        public string StreetName { get; set; }

        public string StreetType { get; set; }

        public string StreetSuffix { get; set; }

        public string PostBoxType { get; set; }

        public string AltStreetName { get; set; }

        public string AltStreetType { get; set; }

        public string AltStreetSuffix { get; set; }

        public string AltLocality { get; set; }

        public string AltPostcode { get; set; }

        public string UnknownData { get; set; }
    }
}