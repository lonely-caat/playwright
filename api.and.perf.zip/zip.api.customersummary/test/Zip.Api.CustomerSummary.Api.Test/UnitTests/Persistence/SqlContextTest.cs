using System;
using System.Collections.Generic;
using System.Data;
using AutoFixture;
using Dapper;
using Moq;
using Moq.Dapper;
using Xunit;
using Zip.Api.CustomerSummary.Persistence;
using Zip.Api.CustomerSummary.Persistence.Interfaces;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Persistence
{
    public class SqlContextTest
    {
        private readonly Mock<IDbConnectionProvider> _dbConnectionProvider;
        private readonly Mock<IDbConnection> _dbConnection;
        private readonly IFixture _fixture;

        public SqlContextTest()
        {
            _fixture = new Fixture();

            _dbConnectionProvider = new Mock<IDbConnectionProvider>();
            _dbConnection = new Mock<IDbConnection>();

            _dbConnection.SetupDapperAsync(x =>
                x.QueryAsync<object>(   
                    It.IsAny<string>(), 
                It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
                .ReturnsAsync(new List<object>());

            _dbConnectionProvider.Setup(x => x.GetDbConnection())
                .Returns(_dbConnection.Object);
        }

        [Fact]
        public void Given_NullConnectionString_ShouldThrow_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlContext(null));
            Assert.Throws<ArgumentNullException>(() => new SqlContext(string.Empty));
        }

        [Fact]
        public void Given_ConnectionString_ShouldCreate()
        {
            Assert.NotNull(new SqlContext("test"));
        }
    }
}
