namespace Zip.Api.CustomerSummary.Application.Statements.Models
{
    public class GenerateStatementResponse
    {
        public GenerateStatementResponse(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }
        
        public bool IsSuccessful { get; set; }
    }
}
