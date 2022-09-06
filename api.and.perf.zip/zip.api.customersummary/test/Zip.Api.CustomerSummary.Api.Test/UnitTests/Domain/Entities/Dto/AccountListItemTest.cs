using System;
using AutoFixture;
using Xunit;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Domain.Entities.Dto
{
    public class AccountListItemTest
    {
        private readonly IFixture _fixture;

        public AccountListItemTest()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_equal()
        {
            var id = _fixture.Create<long>();
            var consumerId = _fixture.Create<long>();
            var publicConsumerId = _fixture.Create<Guid>();
            var productClassification = _fixture.Create<string>();
            var activationDate = _fixture.Create<DateTime>();
            var originationMerchantId = _fixture.Create<long>();
            var originationMerchantNme = _fixture.Create<string>();
            var originationBranchId = _fixture.Create<long>();
            var originationBranchNme = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var email = _fixture.Create<string>();
            var phoneNumber = _fixture.Create<string>();
            var creditStateType = _fixture.Create<CreditProfileStateType>();
            var merchantId = _fixture.Create<long>();
            var merchantUserId = _fixture.Create<long>();

            var item = new AccountListItem()
            {
                Id = id,
                ConsumerId = consumerId,
                PublicConsumerId = publicConsumerId,
                ProductClassification = productClassification,
                ActivationDate = activationDate,
                OriginationMerchantId = originationMerchantId,
                OriginationMerchantName = originationMerchantNme,
                OriginationBranchId = originationBranchId,
                OriginationBranchName = originationBranchNme,
                FirstName = firstName,
                LastName = lastName,
                CountryId = "AU",
                CreditStateType = creditStateType,
                Email = email,
                PhoneNumber = phoneNumber,
                MerchantId = merchantId,
                MerchantUserId = merchantUserId
            };

            Assert.Equal(id, item.Id);
            Assert.Equal(consumerId, item.ConsumerId);
            Assert.Equal(publicConsumerId, item.PublicConsumerId);
            Assert.Equal(productClassification, item.ProductClassification);
            Assert.Equal(activationDate, item.ActivationDate);
            Assert.Equal(originationMerchantId, item.OriginationMerchantId);
            Assert.Equal(originationMerchantNme, item.OriginationMerchantName);
            Assert.Equal(originationBranchId, item.OriginationBranchId);
            Assert.Equal(originationBranchNme, item.OriginationBranchName);
            Assert.Equal(firstName, item.FirstName);
            Assert.Equal(lastName, item.LastName);
            Assert.Equal(email, item.Email);
            Assert.Equal(phoneNumber, item.PhoneNumber);
            Assert.Equal(creditStateType, item.CreditStateType);
            Assert.Equal(merchantId, item.MerchantId);
            Assert.Equal(merchantUserId, item.MerchantUserId);
        }
    }
}
