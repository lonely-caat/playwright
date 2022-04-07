using System.Collections.Generic;

namespace Zip.Api.CustomerSummary.Domain.Common
{
    public class Pagination<T>
    {
        public long TotalCount { get; set; }

        public long Current { get; set; }

        public long PageSize { get; set; } = 100;

        public long TotalPages => TotalCount % PageSize == 0 ? (TotalCount / PageSize) : (TotalCount / PageSize) + 1;

        public IEnumerable<T> Items { get; set; }
    }
}
