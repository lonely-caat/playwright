using GraphQL.Types;

namespace Zip.Api.CustomerProfile.GraphQL.Types
{
    public class ProductClassification : EnumerationGraphType
    {
        public ProductClassification()
        {
            AddValue("ZipPay", "ZipPay", 0);
            AddValue("ZipMoney", "ZipMoney", 1);
            AddValue("ZipBiz", "ZipBiz", 2);
            AddValue("ZipBizBasic", "ZipBizBasic", 3);
        }
    }
}