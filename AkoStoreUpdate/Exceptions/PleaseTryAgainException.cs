namespace AkoStoreUpdate.Exceptions;

class PleaseTryAgainException : Exception
{
    public PleaseTryAgainException() : base("Please try again later.") { }
    public PleaseTryAgainException(string message) : base(message) { }
}
