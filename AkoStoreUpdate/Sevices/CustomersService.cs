using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;
using AkoStoreUpdate.Sevices.Abstract;

namespace AkoStoreUpdate.Sevices;

public class CustomersService : BaseService,ICustomerService
{
    public CustomersService(AkoStoreDatabase database) : base(database)
    {
    }

    public void Add(Customer item)
    {
        _database.Customers.Add(item);
    }

    public void Delete(Guid id)
    {
        var customer = GetById(id);
        if (customer != null)
        {
            _database.Customers.Remove(customer);
        }
    }

    public List<Customer> GetAll()
    {
        return _database.Customers;
    }

    public Customer GetById(Guid id)
    {
        return _database.Customers.FirstOrDefault(c => c.Id == id);
    }

    public void Update(Customer item)
    {
        var customer = GetById(item.Id);
        if (customer != null)
        {
            customer.Name = item.Name;
            customer.Surname = item.Surname;
            customer.Orders = item.Orders;
        }
    }
}