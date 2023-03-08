using Errors.Abstractions;

namespace Errors;

public class BadRequestError : ErrorException
{
    public BadRequestError(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}