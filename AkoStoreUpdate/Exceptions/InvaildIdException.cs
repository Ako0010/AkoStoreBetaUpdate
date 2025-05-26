namespace AkoStoreUpdate.Exceptions;

public class InvaildIdException : Exception
{
    public InvaildIdException() : base("Invalid GUID")
    {
    }

    public InvaildIdException(string? message) : base(message)
    {
    }

    public InvaildIdException(string message, Exception Exceptionn) : base(message, Exceptionn)
    {
    }
}