using System;
using AutoFixture;
using Zip.Api.MerchantDataEnrichment.Test.Common;
using Zip.MerchantDataEnrichment.Domain.Entities;
using Zip.MerchantDataEnrichment.Persistence.DbContexts;

namespace Zip.Api.MerchantDataEnrichment.Test.Factories
{
    internal static class SeedData
    {
        public static void PopulateTestData(this MerchantDataEnrichmentDbContext dbContext)
        {
            dbContext.PopulateMerchantDetailTable();
            dbContext.PopulateMerchantLinkTable();
            dbContext.PopulateCustomerDetailTable();
            dbContext.PopulateVcnTransactionTable();
            dbContext.PopulateMerchantCardAcceptorLocatorTable();
            dbContext.PopulateOpenLoopProduct();
            dbContext.PopulateMerchantOpenLoopProduct();
            dbContext.FeeReference();
        }

        public static void PopulateMerchantDetailTable(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var merchantDetail = fixture.Build<MerchantDetail>()
                                        .With(x => x.Id, new Guid(Constants.MerchantDetailId))
                                        .With(x => x.Postcode, "870")
                                        .With(x => x.CreatedDateTime, DateTime.Now)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .Without(x => x.VcnTransactions)
                                        .Without(x => x.MerchantCardAcceptorLocators)
                                        .Without(x => x.MerchantLinks)
                                        .Without(x => x.MerchantOpenLoopProducts)
                                        .Create();
            var merchantDetail3 = fixture.Build<MerchantDetail>()
                                         .With(x => x.Id, new Guid(Constants.MerchantDetailId3))
                                         .With(x => x.CreatedDateTime, DateTime.Now)
                                         .With(x => x.UpdatedDateTime, DateTime.Now)
                                         .Without(x => x.VcnTransactions)
                                         .Without(x => x.MerchantCardAcceptorLocators)
                                         .Without(x => x.MerchantLinks)
                                         .Without(x => x.MerchantOpenLoopProducts)
                                         .Create();

            dbContext.MerchantDetails.AddRange(merchantDetail, merchantDetail3);
            dbContext.SaveChanges();
        }

        public static void PopulateMerchantLinkTable(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var merchantLink = fixture.Build<MerchantLink>()
                                      .With(x => x.Id, new Guid(Constants.MerchantLinkId))
                                      .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                      .With(x => x.ZipMerchantId, 1)
                                      .With(x => x.ZipBranchId, 1)
                                      .With(x => x.ZipCompanyId, 1)
                                      .With(x => x.Active, true)
                                      .With(x => x.CreatedDateTime, DateTime.Now)
                                      .With(x => x.UpdatedDateTime, DateTime.Now)
                                      .Without(x => x.MerchantDetail)
                                      .Create();

            dbContext.MerchantLinks.Add(merchantLink);
            dbContext.SaveChanges();
        }

        public static void PopulateCustomerDetailTable(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var customerDetail = fixture.Build<CustomerDetail>()
                                        .With(x => x.AccountId, Constants.CustomerDetailAccountId)
                                        .With(x => x.CustomerId, new Guid(Constants.CustomerDetailCustomerId))
                                        .With(x => x.CreatedDateTime, DateTime.Now)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .Without(x => x.VcnTransactions)
                                        .Create();

            dbContext.CustomerDetails.Add(customerDetail);
            dbContext.SaveChanges();
        }

        public static void PopulateVcnTransactionTable(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var vcnTransaction = fixture.Build<VcnTransaction>()
                                        .With(x => x.Id, new Guid(Constants.VcnTransactionId))
                                        .With(x => x.CardAcceptorName, $"{Constants.CardAcceptorName}")
                                        .With(x => x.CardAcceptorCity, $"{Constants.CardAcceptorCity}")
                                        .With(x => x.CreatedDateTime, DateTime.Now)
                                        .With(x => x.UpdatedDateTime, DateTime.Now)
                                        .With(x => x.WebhookId, Constants.WebhookId1)
                                        .Without(x => x.MerchantDetailId)
                                        .Without(x => x.MerchantDetail)
                                        .Without(x => x.CustomerDetailId)
                                        .Without(x => x.CustomerDetail)
                                        .Create();

            dbContext.VcnTransactions.Add(vcnTransaction);
            dbContext.SaveChanges();
        }

        public static void PopulateMerchantCardAcceptorLocatorTable(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var merchantCardAcceptorLocator = fixture.Build<MerchantCardAcceptorLocator>()
                                                    .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                                    .With(x => x.CardAcceptorLocator, $"{Constants.CardAcceptorName} {Constants.CardAcceptorCity}")
                                                    .With(x => x.CreatedDateTime, DateTime.Now)
                                                    .With(x => x.UpdatedDateTime, DateTime.Now)
                                                    .Without(x => x.MerchantDetail)
                                                    .Create();

            dbContext.MerchantCardAcceptorLocators.Add(merchantCardAcceptorLocator);
            dbContext.SaveChanges();
        }

        public static void PopulateOpenLoopProduct(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var openLoopProduct1 = fixture.Build<OpenLoopProduct>()
                                          .With(x => x.Id, new Guid(Constants.OpenLoopProductId1))
                                          .With(x => x.IsActive, true)
                                          .With(x => x.ServiceFeePercentage, 0.05m)
                                          .With(x => x.TransactionAmountLowerLimit, 0m)
                                          .With(x => x.TransactionAmountUpperLimit, decimal.MaxValue)
                                          .With(x => x.TransactionFee, 0.16m)
                                          .Without(x => x.MerchantOpenLoopProducts)
                                          .Without(x => x.FeeReferences)
                                          .Create();

            var openLoopProduct3 = fixture.Build<OpenLoopProduct>()
                                          .With(x => x.Id, new Guid(Constants.OpenLoopProductId3))
                                          .With(x => x.IsActive, true)
                                          .With(x => x.ServiceFeePercentage, 0.10m)
                                          .With(x => x.TransactionAmountLowerLimit, 0m)
                                          .With(x => x.TransactionAmountUpperLimit, decimal.MaxValue)
                                          .With(x => x.TransactionFee, 0.32m)
                                          .Without(x => x.MerchantOpenLoopProducts)
                                          .Without(x => x.FeeReferences)
                                          .Create();

            dbContext.OpenLoopProducts.AddRange(openLoopProduct1, openLoopProduct3);
            dbContext.SaveChanges();
        }

        public static void PopulateMerchantOpenLoopProduct(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var merchantOpenLoopProduct1 = fixture.Build<MerchantOpenLoopProduct>()
                                                  .With(x => x.Id, new Guid(Constants.MerchantOpenLoopProductId1))
                                                  .With(x => x.ZipMerchantId, 1)
                                                  .With(x => x.MerchantDetailId, new Guid(Constants.MerchantDetailId))
                                                  .With(x => x.OpenLoopProductId, new Guid(Constants.OpenLoopProductId3))
                                                  .Without(x => x.OpenLoopProduct)
                                                  .Without(x => x.MerchantDetail)
                                                  .Create();

            dbContext.MerchantOpenLoopProducts.Add(merchantOpenLoopProduct1);
            dbContext.SaveChanges();
        }

        public static void FeeReference(this MerchantDataEnrichmentDbContext dbContext)
        {
            var fixture = new Fixture();
            var feeReference1 = fixture.Build<FeeReference>()
                                       .With(x => x.Id, new Guid(Constants.FeeReferenceId1))
                                       .With(x => x.WebhookId, Constants.WebhookId1)
                                       .With(x => x.OpenLoopProductId, new Guid(Constants.OpenLoopProductId3))
                                       .Without(x => x.OpenLoopProduct)
                                       .Create();

            dbContext.FeeReferences.Add(feeReference1);
            dbContext.SaveChanges();
        }
    }
}