using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Zip.Api.CustomerSummary.Api;
using Zip.Api.CustomerSummary.Application.Statements.Command.GenerateStatement;
using Zip.Api.CustomerSummary.Infrastructure.Common.Extensions;

namespace Zip.Api.CustomerSummary.AcceptanceTests.Steps
{
    [Binding]
    public class GenerateStatementsSteps : BaseStep
    {
        public GenerateStatementsSteps(TestWebApplicationFactory<TestStartup> factory) : base(factory)
        {
        }

        [When(@"I make a post request to /statements with following test cases")]
        public async Task WhenIMakeAPostRequestToStatementsWithFollowingTestCases(Table table)
        {
            foreach (var testItem in table.Rows)
            {
                var accountId = long.Parse(testItem["accountId"]);
                var generateStatementCommand = new GenerateStatementCommand
                {
                    AccountId = accountId,
                    StartDate = DateTime.TryParse(testItem["startDate"], out var sd) ? sd : DateTime.Now.AddDays(-88),
                    EndDate = DateTime.TryParse(testItem["endDate"], out var ed) ? ed : DateTime.Now.AddDays(1)
                };
                
                response = await _httpClient.PostAsync("/api/statements", generateStatementCommand.ToJsonHttpContent());

                Compare(testItem);
            }
        }
    }
}
