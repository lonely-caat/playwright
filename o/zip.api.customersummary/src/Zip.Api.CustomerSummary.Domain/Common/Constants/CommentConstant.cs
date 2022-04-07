namespace Zip.Api.CustomerSummary.Domain.Common.Constants
{
    public static class CommentConstant
    {
        public const string SystemSkipLockComment = "<NFD Skipped> Detected but skipped by system";
        public const string SystemLockComment = "<NFD Lock> Auto lock by system";
        public const string SystemUnlockComment = "<NFD Unlock> Auto unlock by system";
        public const string NfdUnlockTag = "<NFD Unlock>";
        public const string NfdLockTag = "<NFD Lock>";
    }
}
