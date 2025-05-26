using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices;

public abstract class BaseService
{
    protected AkoStoreDatabase _database;
    public BaseService(AkoStoreDatabase database)
    {
        _database = database;
    }
}
