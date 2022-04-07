using System.Text;

namespace Zip.Api.CustomerSummary.Domain.Entities.Kinesis
{
    public class AddressDetail
    {
        public string UnitNumber { get; set; }

        public string StreetNumber { get; set; }

        public string StreetName { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public string PostBoxType { get; set; }

        public string PostBoxNumber { get; set; }

        public string ComparableAddress => GetFullAddress();

        private string GetFullAddress()
        {
            var addressText = new StringBuilder();

            if (!string.IsNullOrEmpty(UnitNumber))
            {
                addressText.Append($"{UnitNumber}/");
            }

            addressText.Append($"{StreetNumber} {StreetName} {Suburb} {State} {PostCode}");

            return addressText.ToString();
        }
    }
}
