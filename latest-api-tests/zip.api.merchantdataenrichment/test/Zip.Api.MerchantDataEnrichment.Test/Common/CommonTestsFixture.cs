using System;
using System.Threading;
using AutoFixture;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Zip.MerchantDataEnrichment.Persistence.DbContexts;

namespace Zip.Api.MerchantDataEnrichment.Test.Common
{
    public class CommonTestsFixture
    {
        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }

        public Mock<IMediator> MockMediator { get; }

        public Fixture Fixture { get; }

        public IMerchantDataEnrichmentDbContext DbContext { get; set; }

        public CancellationToken CancellationToken { get; }

        public CommonTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(
                cfg => cfg.AddMaps(typeof(Zip.MerchantDataEnrichment.Infrastructure.ServicesConfiguration),
                                   typeof(Zip.MerchantDataEnrichment.Application.ServicesConfiguration)));

            Mapper = ConfigurationProvider.CreateMapper();
            MockMediator = new Mock<IMediator>();
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            CancellationToken = CancellationToken.None;

            ConfigureInMemoryDatabase();
        }

        public void ConfigureInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<MerchantDataEnrichmentDbContext>()
                         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                         .Options;
            DbContext = new MerchantDataEnrichmentDbContext(options);
        }
    }
}
