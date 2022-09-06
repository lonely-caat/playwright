using Nest;
using Zip.CustomerProfile.Data.Interfaces;

namespace Zip.Api.CustomerProfile.Services
{
    public class QueryByVedaReferenceNumber : IQueryBuilder
    {
        private readonly string _vedaReferenceNumber;

        public QueryByVedaReferenceNumber(string vedaReferenceNumber)
        {
            _vedaReferenceNumber = vedaReferenceNumber;
        }

        public QueryContainer GetQuery()
        {
            return new MatchPhraseQuery
            {
                Field = Infer.Field("vedaReferenceNumber"),
                Analyzer = "standard",
                Boost = 1.1,
                Name = "Match Phrase VedaReferenceNumber",
                Query = _vedaReferenceNumber
            };
        }
    }
}