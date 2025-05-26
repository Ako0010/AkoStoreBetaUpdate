

namespace AkoStoreUpdate.Exceptions;

class UsernameMIghtBeTakenException : Exception
{
    public UsernameMIghtBeTakenException() : base("Username might be taken. Please try a different one.") { }
    public UsernameMIghtBeTakenException(string message) : base(message) { }
}
