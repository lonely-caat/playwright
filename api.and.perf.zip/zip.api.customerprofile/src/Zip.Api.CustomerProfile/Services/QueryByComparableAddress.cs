using Nest;
using Zip.CustomerProfile.Data.Interfaces;

namespace Zip.Api.CustomerProfile.Services
{
    public class QueryByComparableAddress : IQueryBuilder
    {
        private readonly string _residentialComparableAddress;

        public QueryByComparableAddress(string residentialComparableAddress)
        {
            _residentialComparableAddress = residentialComparableAddress;
        }

        public QueryContainer GetQuery()
        {
            return new MatchPhraseQuery
            {
                Field = Infer.Field("residentialComparableAddress"),
                Analyzer = "standard",
                Boost = 1.1,
                Name = "Match Phrase ComparableAddress",
                Query = _residentialComparableAddress
            };
        }
    }
}