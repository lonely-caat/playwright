using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Domain.Entities.GoogleAddress
{
    public class GoogleAddressResponse
    {
        public GoogleAddressResponse()
        {
            Predictions = new List<Prediction>();
        }

        public List<Prediction> Predictions { get; set; }

        public string Status { get; set; }

        public string Error_Message { get; set; }
    }

    public class Prediction
    {
        public string Place_Id { get; set; }

        public string Description { get; set; }
    }
}
