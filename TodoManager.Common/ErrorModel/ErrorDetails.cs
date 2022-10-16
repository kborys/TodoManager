namespace TodoManager.Common.ErrorModel;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public ErrorDetails(int statusCode, string? message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
