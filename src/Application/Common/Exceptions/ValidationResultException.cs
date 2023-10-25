namespace Application.Common.Exceptions;

public class ValidationResultException : Exception
{
    public ErrorResult Error { get; }

    public ValidationResultException(ErrorResult error) 
    {
        Error = error;
    }

    public ValidationResultException(string? message, ErrorResult error) : base(message)
    {
        Error = error;
    }

    public ValidationResultException(string? message, Exception? innerException, ErrorResult error) : base(message,
        innerException)
    {
        Error = error;
    }
}