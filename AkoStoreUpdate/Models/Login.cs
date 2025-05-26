


namespace AkoStoreUpdate.Models
{
  public class Login : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
