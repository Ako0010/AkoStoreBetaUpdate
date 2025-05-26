using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices.Absturct;

public interface ILoginService
{
    bool Login(string username, string password);

}
