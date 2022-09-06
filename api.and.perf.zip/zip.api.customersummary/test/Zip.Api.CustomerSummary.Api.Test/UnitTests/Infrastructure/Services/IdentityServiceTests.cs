using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Identity;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService;
using Zip.Api.CustomerSummary.Infrastructure.Services.IdentityService.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure.Services
{
    public class IdentityServiceTests
    {
        private readonly IdentityService _target;

        private readonly IFixture _fixture;
        
        private readonly Mock<IUserManagementProxy> _userManagementProxy;
        
        private readonly IMemoryCache _memoryCache;

        public IdentityServiceTests()
        {
            _fixture = new Fixture();
            _userManagementProxy = new Mock<IUserManagementProxy>();

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            
            _target = new IdentityService(_userManagementProxy.Object, _memoryCache);
        }
        
        [Fact]
        public void Given_Null_UserManagementProxy_DI_Should_Throw_ArgumentNullException()
        {
            Action action = () => { new IdentityService(null, new Mock<IMemoryCache>().Object); };

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Given_Null_MemoryCache_DI_Should_Throw_ArgumentNullException()
        {
            Action action = () => { new IdentityService(_userManagementProxy.Object, null); };

            action.Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Given_Valid_Email_When_GetUserByEmailAsync_Should_Set_MemoryCache_Correctly()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var expect = _fixture.Build<UserDetail>().With(x => x.Email, email).Create();
            _userManagementProxy.Setup(x => x.GetUserByEmailAsync(email))
                                .ReturnsAsync(expect);

            // Action
            var actual = await _target.GetUserByEmailAsync(email);

            // Assert
            _memoryCache.TryGetValue(email, out var cacheValue);
            cacheValue.Should().BeEquivalentTo(expect);
            actual.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public async Task Given_Valid_Email_And_Cache_Set_When_GetUserByEmailAsync_Should_Work_Correctly()
        {
            // Arrange
            var email = _fixture.Create<string>();
            var expect = _fixture.Build<UserDetail>().With(x => x.Email, email).Create();
            _memoryCache.Set(email, expect);
            
            // Action
            var actual = await _target.GetUserByEmailAsync(email);

            // Assert
            actual.Should().BeEquivalentTo(expect);
        }
        
        [Fact]
        public async Task Given_UserManagementProxy_Throws_Exception_When_GetUserByEmailAsync_Should_Return_Null()
        {
            // Arrange
            var email = _fixture.Create<string>();
            _userManagementProxy.Setup(x => x.GetUserByEmailAsync(email))
                                .ThrowsAsync(new Exception());

            // Action
            var actual = await _target.GetUserByEmailAsync(email);

            // Assert
            actual.Should().BeNull();
        }
    }
}
