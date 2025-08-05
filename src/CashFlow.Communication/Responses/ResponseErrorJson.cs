namespace CashFlow.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorJson(List<string> errorMessages)
    {
        this.ErrorMessages = errorMessages;
    }

    public ResponseErrorJson(string errorMessage)
    {
        this.ErrorMessages = [errorMessage];
    }
}