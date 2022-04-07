namespace Zip.Api.CustomerSummary.Domain.Common.Constants
{
    public static class TrustScore
    {
        public const int MaximumNegative = -100;

        public const int MaximumPositive = 100;

        public const int InitialValue = 0;

        public const int NegativeScore20 = -20; // NFD because of Velocity rule, can auto unlock

        public const int NegativeScore25 = -25; // NFD because of Matching rules but low risk, can auto unlock

        public const int NegativeScore26 = -26; // NFD because of Matching rules but no balance and no matching active customer, cannot auto unlock 

        public const int NegativeScore30 = -30; // NFD because of Matching rules with Bad Account, cannot auto unlock 

        public const int NegativeScore40 = -40; // NFD because of Velocity rule within hours, cannot auto unlock

        public const int Score20 = 20;

        public const int Score25 = 25;

        public const int Score30 = 30;
    }
}
