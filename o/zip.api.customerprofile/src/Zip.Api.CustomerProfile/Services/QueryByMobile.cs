using Nest;
using Zip.CustomerProfile.Data.Interfaces;

namespace Zip.Api.CustomerProfile.Services
{
    public class QueryByMobile : IQueryBuilder
    {
        private readonly string _mobile;

        public QueryByMobile(string mobile)
        {
            _mobile = mobile;
        }

        public QueryContainer GetQuery()
        {
            return new MatchPhraseQuery
            {
                Field = Infer.Field("mobile"),
                Analyzer = "standard",
                Boost = 1.1,
                Name = "Match Phrase Mobile",
                Query = _mobile
            };
        }
    }
}