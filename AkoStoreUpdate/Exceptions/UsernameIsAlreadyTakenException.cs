
namespace AkoStoreUpdate.Exceptions;

class UsernameIsAlreadyTakenException : Exception
{
    public UsernameIsAlreadyTakenException() : base("Username is already taken") { }
    public UsernameIsAlreadyTakenException(string? message) : base(message) { }
}
