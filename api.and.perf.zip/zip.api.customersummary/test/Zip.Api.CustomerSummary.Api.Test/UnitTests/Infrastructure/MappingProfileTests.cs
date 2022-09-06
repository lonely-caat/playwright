using System;
using System.Reflection;
using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Zip.Api.CustomerSummary.Application.Accounts.Command.CloseAccount;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Api.CustomerSummary.Domain.Entities.Consumers;
using Zip.Api.CustomerSummary.Domain.Entities.Dto;
using Zip.Api.CustomerSummary.Domain.Entities.Kinesis;
using Zip.Api.CustomerSummary.Infrastructure;
using Zip.Services.Accounts.Contract.Account;
using Zip.Services.Accounts.Contract.Tango;
using ZipMoney.Services.Payments.Contract.PaymentMethods;
using ZipMoney.Services.Payments.Contract.Payments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Infrastructure
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;

        public MappingProfileTests()
        {
            var assemblyServiceType = typeof(CloseAccountCommandHandler).GetTypeInfo();
            var assemblyServiceInfo = assemblyServiceType.Assembly;

            var services = new ServiceCollection();
            services.AddAutoMapper(assemblyServiceInfo, typeof(ServicesConfiguration).GetTypeInfo().Assembly);

            var sp = services.BuildServiceProvider();
            _mapper = sp.GetService<IMapper>();
            
            _fixture = new Fixture();
        }

        [Fact]
        public void Should_MapTo_AddressDetail()
        {
            var expectedPostCode = _fixture.Create<string>();
            var expectedUnitNumber = _fixture.Create<string>();
            var expectedSuburb = _fixture.Create<string>();
            var expectedStreetName = _fixture.Create<string>();
            var expectedStreetNumber = _fixture.Create<string>();
            var expectedState = _fixture.Create<string>();

            var result = _mapper.Map<Address, AddressDetail>(new Address
            {
                PostCode = expectedPostCode,
                UnitNumber = expectedUnitNumber,
                Suburb = expectedSuburb,
                StreetName = expectedStreetName,
                StreetNumber = expectedStreetNumber,
                State = expectedState
            });

            Assert.IsType<AddressDetail>(result);
            Assert.Equal(expectedState, result.State);
            Assert.Equal(expectedPostCode, result.PostCode);
            Assert.Equal(expectedStreetName, result.StreetName);
            Assert.Equal(expectedStreetNumber, result.StreetNumber);
            Assert.Equal(expectedSuburb, result.Suburb);
            Assert.Equal(expectedUnitNumber, result.UnitNumber);
        }

        [Fact]
        public void Should_MapTo_LmsAccountDto()
        {
            var expCreditStatus = _fixture.Create<string>();
            var expCreditLimit = _fixture.Create<decimal>();
            var expInterestFreeBalance = _fixture.Create<decimal>();
            var expBalance = _fixture.Create<decimal>();
            var expPayoutQuote = _fixture.Create<decimal>();
            var expDaysInArrears = _fixture.Create<int>();
            var expArrearsBalance = _fixture.Create<decimal>();
            var expAccountWithCheckDigit = _fixture.Create<int>();
            var expAvailableFunds = _fixture.Create<decimal>();
            var expContractualAmount = _fixture.Create<decimal>();
            var expContractualDate = _fixture.Create<DateTime>().ToLongDateString();

            var result = _mapper.Map<AccountResponse, LmsAccountDto>(
                new AccountResponse
                {
                    Configuration = new AccountResponseConfiguration { CreditLimit = expCreditLimit },
                    LoanMgtAccount = new LoanMgtAccount
                    {
                        GeneralPurpose1 = expCreditStatus,
                        InterestFreeBalance = expInterestFreeBalance,
                        PayoutQuote = expPayoutQuote,
                        DaysInArrearsAsAt = expDaysInArrears,
                        ArrearsBalanceAsAt = expArrearsBalance,
                        AccountWithCheckDigit = expAccountWithCheckDigit,
                        ContractualAmountDueAsAt = expContractualAmount,
                        ContractualNextDueDateAsAt = expContractualDate
                    },
                    Balance = expBalance,
                    AvailableBalance = expAvailableFunds
                },
                options => { options.Items["CreditStatus"] = expCreditStatus; });

            Assert.IsType<LmsAccountDto>(result);
            Assert.Equal(expCreditStatus, result.CreditStatus);
            Assert.Equal(expCreditLimit, result.CreditLimit);
            Assert.Equal(expInterestFreeBalance, result.InterestFreeBalance);
            Assert.Equal(expBalance, result.CurrentBalance);
            Assert.Equal(expDaysInArrears, result.DaysInArrears);
            Assert.Equal(expArrearsBalance, result.ArrearsBalance);
            Assert.Equal(expAccountWithCheckDigit, result.AccountWithCheckDigit);
            Assert.Equal(expAvailableFunds, result.AvailableFunds);
            Assert.Equal(expContractualAmount, result.ContractualAmount);
            Assert.Equal(DateTime.Parse(expContractualDate), result.ContractualDate);
        }

        [Fact]
        public void Should_MapTo_PaymentDto()
        {
            var expCountryCode = _fixture.Create<ZipMoney.Services.Payments.Contract.Types.CountryCode>();

            var result = _mapper.Map<PaymentResponse, PaymentDto>(new PaymentResponse
            {
                CountryCode = expCountryCode
            });

            Assert.IsType<PaymentDto>(result);
            Assert.Equal(expCountryCode, result.CountryCode);
        }

        [Fact]
        public void Should_MapTo_PaymentMethodDto()
        {
            var expCountryCode = _fixture.Create<ZipMoney.Services.Payments.Contract.Types.CountryCode>();

            var result = _mapper.Map<PaymentMethodResponse, PaymentMethodDto>(new PaymentMethodResponse
            {
                CountryCode = expCountryCode
            });

            Assert.IsType<PaymentMethodDto>(result);
            Assert.Equal(expCountryCode, result.CountryCode);
        }
    }
}
