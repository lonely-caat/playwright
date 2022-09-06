using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zip.Api.CustomerSummary.Persistence.DbContexts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Persistence
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["dbConnString"];

            return services.AddSingleton<IDbContext>(sp => new SqlContext(connectionString))
                           .AddTransient<IUnitOfWork>(sp => new SqlUnitOfWork(connectionString))
                           .AddSingleton<ICountryContext, CountryContext>()
                           .AddSingleton<IProductContext, ProductContext>()
                           .AddSingleton<ICrmCommentContext, CrmCommentContext>()
                           .AddSingleton<IAccountContext, AccountContext>()
                           .AddSingleton<IAttributeContext, AttributeContext>()
                           .AddSingleton<ICustomerAttributeContext, CustomerAttributeContext>()
                           .AddSingleton<IAddressContext>(sp => new AddressContext(sp.GetRequiredService<IDbContext>(), sp.GetRequiredService<ICountryContext>()))
                           .AddSingleton<IConsumerContext, ConsumerContext>()
                           .AddSingleton<IApplicationEventContext, ApplicationEventContext>()
                           .AddSingleton<IContactContext, ContactContext>()
                           .AddSingleton<ITransactionHistoryContext, TransactionHistoryContext>()
                           .AddSingleton<IConsumerOperationRequestContext, ConsumerOperationRequestContext>()
                           .AddSingleton<IAccountTypeContext, AccountTypeContext>()
                           .AddSingleton<ICreditProfileContext, CreditProfileContext>()
                           .AddSingleton<IMerchantContext, MerchantContext>()
                           .AddSingleton<IConsumerStatContext, ConsumerStatContext>()
                           .AddSingleton<IMfaContext, MfaContext>()
                           .AddSingleton<IMessageLogContext, MessageLogContext>()
                           .AddSingleton<IPhoneContext, PhoneContext>()
                           .AddSingleton<IServiceEventContext, ServiceEventContext>()
                           .AddSingleton<IStatementContext, StatementContext>()
                           .AddSingleton<ISmsContentContext, SmsContentContext>()
                           .AddSingleton<IPayNowAccountContext, PayNowAccountContext>();
        }
    }
}
