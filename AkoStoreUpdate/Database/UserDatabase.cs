using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Database
{
    public static class UserDatabase
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Email = "admin@example.com" }
        };
    }
}