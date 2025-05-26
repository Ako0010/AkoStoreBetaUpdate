using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices.Absturct;

public interface IRegisterService
{
    bool Register(string username, string password, string email);
}
