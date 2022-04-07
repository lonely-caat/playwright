using FluentValidation.TestHelper;
using Xunit;
using Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;

namespace Zip.Api.CustomerSummary.Api.Test.UnitTests.Validators.Comments
{
    public class CreateCrmCommentCommandValidatorTest
    {
        private readonly CreateCrmCommentCommandValidator _validator;

        public CreateCrmCommentCommandValidatorTest()
        {
            _validator = new CreateCrmCommentCommandValidator();
        }

        [Fact]
        public void Given_ReferenceIdInvalid_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ReferenceId, 0);
        }

        [Fact]
        public void Given_DetailNullOrEmpty_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Detail, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.Detail, string.Empty);
        }

        [Fact]
        public void Given_CategoryNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Category, null as CommentCategory?);
        }

        [Fact]
        public void Given_CommentTypeNull_Should_have_error()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.Type, null as CommentType?);
        }

        [Fact]
        public void Given_AllGood_Should_Pass()
        {
            var result = _validator.Validate(new CreateCrmCommentCommand()
            {
                ReferenceId = 202,
                Detail = "test",
                CommentBy = "Shan Ke",
                Category = CommentCategory.AccountEnquiry,
                Type = CommentType.Consumer
            });

            Assert.True(result.IsValid);
        }
    }
}
