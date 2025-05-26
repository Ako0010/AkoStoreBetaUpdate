namespace AkoStoreUpdate.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found.") { }

    public ProductNotFoundException(string message) : base(message) { }
    public ProductNotFoundException(Guid id,string message) : base(message) { }

}
