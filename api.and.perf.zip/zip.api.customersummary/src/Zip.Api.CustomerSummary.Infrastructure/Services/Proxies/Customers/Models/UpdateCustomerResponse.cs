using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.Proxies.Customers.Models
{
    public class UpdateCustomerResponse : BaseResponse
    {
        public string Id { get; set; }
        public IEnumerable<long> ConsumerIdsWithMatchingContact { get; set; }
        public bool DuplicatedContactsExist { get; set; }

        public static T CreateForDuplicate<T>(string id, IEnumerable<long> duplicates, string message) where T : UpdateCustomerResponse, new()
        {
            return new T()
            {
                Success = false,
                Id = id,
                ConsumerIdsWithMatchingContact = duplicates,
                Message = message,
                DuplicatedContactsExist = true
            };
        }

        public static T CreateForSuccess<T>(string id) where T : UpdateCustomerResponse, new()
        {
            return new T()
            {
                Success = true,
                Id = id
            };
        }
    }
}
