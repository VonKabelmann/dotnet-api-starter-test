namespace dotnet_api_test.Exceptions.ExceptionResponses;

public class ConflictExceptionResponse : HttpExceptionResponse
{
    public ConflictExceptionResponse(string msg, int statusCode = StatusCodes.Status409Conflict) : base(statusCode, msg)
    {
        StatusCode = statusCode;
    }
}