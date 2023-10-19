using System.Net;

namespace Application.Common;

public class ErrorResult
{
    public string Message { get; }
    public string Description { get; }
    public HttpStatusCode StatusCode { get; }

    public ErrorResult(string message, string description, HttpStatusCode statusCode)
    {
        Message = message;
        Description = description;
        StatusCode = statusCode;
    }

    public static readonly ErrorResult GenericError =
        new ErrorResult(
            message: "Server error",
            description: "An internal server error has occured",
            statusCode: HttpStatusCode.InternalServerError);
}

