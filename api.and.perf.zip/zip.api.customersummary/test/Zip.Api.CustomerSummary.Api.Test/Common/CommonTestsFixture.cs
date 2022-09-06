using System.Net.Http;
using System.Threading;
using AutoFixture;
using AutoMapper;
using MediatR;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Test.Factories;
using Zip.Api.CustomerSummary.Application.VcnCards.Mapper;
using InfrastructureServicesConfiguration = Zip.Api.CustomerSummary.Infrastructure.ServicesConfiguration;

namespace Zip.Api.CustomerSummary.Api.Test.Common
{
    public class CommonTestsFixture : IClassFixture<ApiFactory>
    {
        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }

        public Mock<IMediator> MockMediator { get; }

        public Fixture Fixture { get; }

        public CancellationToken CancellationToken { get; }

        protected HttpClient Client { get; }

        public CommonTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(
                cfg => cfg.AddMaps(typeof(VcnCardsProfile), typeof(InfrastructureServicesConfiguration), typeof(Startup)));

            Mapper = ConfigurationProvider.CreateMapper();
            MockMediator = new Mock<IMediator>();
            Fixture = new Fixture();
            CancellationToken = CancellationToken.None;
        }

        public CommonTestsFixture(ApiFactory factory)
        {
            ConfigurationProvider = new MapperConfiguration(
                    cfg => cfg.AddMaps(typeof(VcnCardsProfile), typeof(InfrastructureServicesConfiguration), typeof(Startup)));

            Mapper = ConfigurationProvider.CreateMapper();
            MockMediator = new Mock<IMediator>();
            Fixture = new Fixture();
            CancellationToken = CancellationToken.None;
            Client = factory.CreateClient();
        }
    }
}
