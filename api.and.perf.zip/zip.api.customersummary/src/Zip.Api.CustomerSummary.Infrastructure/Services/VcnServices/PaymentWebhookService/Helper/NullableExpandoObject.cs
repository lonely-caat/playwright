using System.Collections.Generic;
using System.Dynamic;

namespace Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.PaymentWebhookService.Helper
{
    // This class will set a property to null if a dynamic property does not exist and swallow RuntimeBinderException
    public class NullableExpandoObject : DynamicObject
    {
        private readonly Dictionary<string, object> values
            = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            values.TryGetValue(binder.Name, out result);
            return true; // Return true surpresses the exception
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            values[binder.Name] = value;
            return true;
        }
    }
}
