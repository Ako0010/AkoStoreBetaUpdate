namespace AkoStoreUpdate.Exceptions;

class ProductNotFoundIdException : Exception
{
    public ProductNotFoundIdException() : base("Product ID not found.") { }

    public ProductNotFoundIdException(string message) : base(message) { }
    public ProductNotFoundIdException(Guid id, string message) : base(message) { }
}
