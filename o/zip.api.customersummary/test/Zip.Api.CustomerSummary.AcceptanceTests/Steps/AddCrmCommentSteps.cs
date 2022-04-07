using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Comments.Command.CreateCrmComment;
using Zip.Api.CustomerSummary.Domain.Entities.Comments;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class AddCrmCommentSteps : BaseStep
    {
        public AddCrmCommentSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /crmcomments with following test cases")]
        public async Task WhenIMakeAPostRequestToCrmcommentsWithFollowingTestCases(Table table)
        {
            try
            {
                foreach (var testItem in table.Rows)
                {
                    var createCrmCommentCommand = new CreateCrmCommentCommand
                    {
                        ReferenceId = long.Parse(testItem["referenceId"]),
                        Category = Enum.TryParse<CommentCategory>(testItem["category"], out var cc) ? cc : null as CommentCategory?,
                        Detail = testItem["detail"],
                        CommentBy = testItem["commentby"],
                        Type = Enum.TryParse<CommentType>(testItem["type"], out var ct) ? ct : null as CommentType?
                    };
                    
                    response = await _httpClient.PostAsync("/api/crmcomments", createCrmCommentCommand.ToJsonHttpContent());

                    Assert.Equal(Enum.Parse<HttpStatusCode>(testItem["expected"]), response.StatusCode);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
