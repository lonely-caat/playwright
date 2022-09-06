using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Api.Extensions;
using Zip.Api.CustomerSummary.Api.Test.Common;
using Zip.Api.CustomerSummary.Application.VcnCards.Query.GetCard;
using Zip.Api.CustomerSummary.Infrastructure;
using Zip.Api.CustomerSummary.Infrastructure.Services.ApplicationEventService.Interfaces;
using Zip.Api.CustomerSummary.Infrastructure.Services.VcnServices.VcnCardService;
using Zip.Api.CustomerSummary.Persistence;
using InfrastructureServicesConfiguration = Zip.Api.CustomerSummary.Infrastructure.ServicesConfiguration;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure
{
    public class ServiceConfigurationTests : CommonTestsFixture
    {
        public ServiceConfigurationTests()
        { 
        }

        [Fact]
        public void When_ServicesAreInjected_Then_AllTheInterfaces_CanBe_Resolved()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .AddJsonFile("appsettings.prod.json")
                               .Build();
            
            var hostEnvironment = new Mock<IWebHostEnvironment>();
            hostEnvironment.Setup(x => x.EnvironmentName)
                           .Returns("local");
            
            var assemblyServiceType = typeof(GetCardQueryHandler).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            var services = new ServiceCollection();

            services.AddMemoryCache()
                    .AddSingleton(configuration)
                    .AddOptionsConfiguration(configuration)
                    .AddInfrastructureServices(configuration, hostEnvironment.Object)
                    .AddPersistenceServices(configuration)
                    .AddAutoMapper(assemblyServiceInfo,
                                   typeof(InfrastructureServicesConfiguration).GetTypeInfo().Assembly,
                                   typeof(Startup).GetTypeInfo().Assembly);

            // Action
            var serviceProvider = services.BuildServiceProvider();
            
            var avoidInterfaces = new [] { typeof(INamingContexts) };

            var interfaces = typeof(VcnCardService).Assembly
                                                   .GetTypes()
                                                   .Where(c => c.IsInterface && c.IsPublic)
                                                   .Where(c => avoidInterfaces.All(i => c != i))
                                                   .ToArray();

            // Assert
            foreach (var assembly in interfaces)
            {
                serviceProvider.GetServices(assembly)
                               .Should()
                               .NotBeEmpty($"{assembly.Name} can't be resolved");
            }
        }
    }
}
