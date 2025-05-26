using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;
using AkoStoreUpdate.Enum;

namespace AkoStoreUpdate.Sevices
{
    class RegisterService
    {
        public RegisterResult Register(string username, string password, string email)
        {
            foreach (var user in UserDatabase.Users)
            {
                if (user.Username == username)
                    return RegisterResult.UsernameTaken;
                if (user.Email == email)
                    return RegisterResult.EmailTaken;
            }

            try
            {
                UserDatabase.Users.Add(new User
                {
                    Username = username,
                    Password = password,
                    Email = email
                });
                return RegisterResult.Success;
            }
            catch
            {
                return RegisterResult.Error;
            }
        }
    }
}