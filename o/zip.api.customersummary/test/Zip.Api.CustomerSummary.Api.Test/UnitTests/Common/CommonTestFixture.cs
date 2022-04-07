using AutoFixture;
using AutoMapper;
using System.Linq;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Common
{
    public class CommonTestFixture
    {
        public readonly Fixture _fixture;

        public readonly IMapper _mapper;

        public CommonTestFixture()
        {
            _fixture = new Fixture();

            // Source: https://github.com/AutoFixture/AutoFixture/issues/667
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(Startup)); });
            mapperConfiguration.AssertConfigurationIsValid();
            _mapper = mapperConfiguration.CreateMapper();
        }
    }
}
