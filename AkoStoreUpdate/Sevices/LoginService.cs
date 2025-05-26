using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices
{
    class LoginService
    {
        public User CurrentUser { get; set; }

        public string GetCurrentUser()
        {
            return CurrentUser?.Username;
        }

        public bool Login(string username, string password)
        {
            var user = UserDatabase.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }
    }
}