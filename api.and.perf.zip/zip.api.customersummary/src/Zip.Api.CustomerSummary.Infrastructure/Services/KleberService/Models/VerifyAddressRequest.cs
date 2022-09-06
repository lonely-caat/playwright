namespace Zip.Api.CustomerSummary.Infrastructure.Services.KleberService.Models
{
    public class VerifyAddressRequest
    {
        public VerifyAddressRequest(
            string unitNumber,
            string streetNumber,
            string streetName,
            string locality,
            string postcode,
            string state)
        {
            AddressLine1 = string.IsNullOrEmpty(unitNumber)
                ? $"{streetNumber} {streetName}"
                : $"{unitNumber}/{streetNumber} {streetName}";
            
            Locality = locality;
            Postcode = postcode;
            State = state;
        }

        public string AddressLine1 { get; }
        
        public string Locality { get; }
        
        public string Postcode { get; }
        
        public string State { get; }
    }
}
