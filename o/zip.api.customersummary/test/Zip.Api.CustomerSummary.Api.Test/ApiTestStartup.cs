using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zip.Api.CustomerSummary.Api.Test.Middleware;
using Zip.Api.CustomerSummary.Api.Test.Mocks.DbContexts;
using Zip.Api.CustomerSummary.Persistence.DbContexts.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test
{
    public class ApiTestStartup : Startup
    {
        public ApiTestStartup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void AddInfrastructureServices(IServiceCollection services)
        {
            services.AddTestInfrastructureServices();
        }

        protected override void AddPersistenceServices(IServiceCollection services)
        {
            services.AddTransient<ICountryContext, MockCountryContext>()
                .AddTransient<IProductContext, MockProductContext>()
                .AddTransient<IAccountContext, MockAccountContext>()
                .AddTransient<IAccountTypeContext, MockAccountTypeContext>()
                .AddTransient<IConsumerContext, MockConsumerContext>()
                .AddTransient<IConsumerStatContext, MockConsumerStatContext>()
                .AddTransient<IMfaContext, MockMfaContext>()
                .AddTransient<ICustomerAttributeContext, MockCustomerAttributeContext>()
                .AddTransient<IAddressContext, MockAddressContext>()
                .AddTransient<IMerchantContext, MockMerchantContext>()
                .AddTransient<ICreditProfileContext, MockCreditProfileContext>()
                .AddTransient<IContactContext, MockContactContext>()
                .AddTransient<ICrmCommentContext, MockCrmCommentContext>()
                .AddTransient<IMessageLogContext, MockMessageLogContext>()
                .AddTransient<ISmsContentContext, MockSmsContentContext>()
                .AddTransient<ITransactionHistoryContext, MockTransactionHistoryContext>()
                .AddTransient<IStatementContext, MockStatementContext>()
                .AddTransient<IAttributeContext, MockAttributeContext>()
                .AddTransient<IPayNowAccountContext, MockPayNowAccountContext>();
        }

        protected override IApplicationBuilder ConfigureApplicationMiddleware(IApplicationBuilder app)
        {
            app.UseMiddleware<AuthenticatedTestRequestMiddleware>();

            return base.ConfigureApplicationMiddleware(app);
        }
    }
}