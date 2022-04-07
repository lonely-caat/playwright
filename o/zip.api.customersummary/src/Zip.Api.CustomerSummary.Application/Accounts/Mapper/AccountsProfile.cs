using System;
using AutoMapper;
using Zip.Api.CustomerSummary.Application.Accounts.Command.AddAttributeAndLockAccount;
using Zip.Api.CustomerSummary.Application.Accounts.Command.LockAccount;
using Zip.Api.CustomerSummary.Domain.Entities.Accounts;
using Zip.Services.Accounts.Contract.Account;

namespace Zip.Api.CustomerSummary.Application.Accounts.Mapper
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<AccountResponse, LmsAccountDto>()
                    .ForMember(dest => dest.CreditStatus,
                        o => o.MapFrom((src, dest, destMember, context) => context.Items["CreditStatus"]))
                    .ForMember(dest => dest.CreditLimit,
                        o => o.MapFrom((source, dest) => source.Configuration?.CreditLimit))
                    .ForMember(dest => dest.InterestFreeBalance,
                        o => o.MapFrom((source, dest) => source.LoanMgtAccount?.InterestFreeBalance))
                    .ForMember(dest => dest.CurrentBalance, o => o.MapFrom(source => source.Balance))
                    .ForMember(dest => dest.PendingBalance, o => o.MapFrom(source => source.PendingBalance))
                    .ForMember(dest => dest.DaysInArrears,
                        o => o.MapFrom((source, dest) => source.LoanMgtAccount?.DaysInArrearsAsAt))
                    .ForMember(dest => dest.ArrearsBalance,
                        o => o.MapFrom((source, dest) => source.LoanMgtAccount?.ArrearsBalanceAsAt))
                    .ForMember(dest => dest.AccountWithCheckDigit,
                        o => o.MapFrom((source, dest) => source.LoanMgtAccount?.AccountWithCheckDigit))
                    .ForMember(dest => dest.AvailableFunds, o => o.MapFrom(source => source.AvailableBalance))
                    .ForMember(dest => dest.ContractualDate,
                        source => source.MapFrom(
                            src =>
                                src.LoanMgtAccount != null && string.IsNullOrEmpty(src.LoanMgtAccount.ContractualNextDueDateAsAt) ?
                                (DateTime?)null :
                                DateTime.Parse(src.LoanMgtAccount.ContractualNextDueDateAsAt)))
                    .ForMember(dest => dest.ContractualAmount, o => o.MapFrom((source, dest) => source.LoanMgtAccount?.ContractualAmountDueAsAt))
                    .ForMember(dest => dest.NextRepaymentDate, source => source.MapFrom(src => src.LoanMgtAccount != null && string.IsNullOrEmpty(src.LoanMgtAccount.DirectDebitNextDateDueAsAt) ? (DateTime?)null : DateTime.Parse(src.LoanMgtAccount.DirectDebitNextDateDueAsAt)));

            CreateMap<AddAttributeAndLockAccountCommand , LockAccountCommand> ();
        }
    }
}
