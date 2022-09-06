namespace Zip.Api.CustomerSummary.Domain.Entities.Consumers
{
    public enum ConsumerAttributeEnum
    {
        Fraud = 1,

        SuspectedFraud = 2,

        Bankrupt = 3,

        PartIx = 4,

        Loa = 5,

        Deceased = 6,

        WrittenOffLoss = 7,

        Hardship = 8,

        Uncontactable = 9,

        NoFurtherDrawdown = 10,

        PaymentDefault = 11,

        Section88 = 12,

        Section21D = 13,

        ReturnMail = 14,

        Chargeback = 15,

        BadCreditMatch = 16,

        Settled = 100,

        HighRisk = 101,

        MediumRisk = 102,

        LowRisk = 103
    }
}
