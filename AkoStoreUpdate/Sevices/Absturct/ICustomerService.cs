using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices.Abstract;

public interface ICustomerService
{
    public List<Customer> GetAll();
    public Customer GetById(Guid id);
    public void Add(Customer item);
    public void Update(Customer item);
    public void Delete(Guid id);
}
