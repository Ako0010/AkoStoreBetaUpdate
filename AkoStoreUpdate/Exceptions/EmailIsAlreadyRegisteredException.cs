
namespace AkoStoreUpdate.Exceptions;

class EmailIsAlreadyRegisteredException : Exception
{
    public EmailIsAlreadyRegisteredException() : base("Email is already registered.") { }
    public EmailIsAlreadyRegisteredException(string message) : base(message) { }
}
