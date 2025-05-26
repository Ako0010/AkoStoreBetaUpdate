using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices.Abstract;

public interface IOrderService
{
    public List<Order> GetAll();
    public Order GetById(Guid id);
    public void Add(Order item);
    public void Update(Order item);
    public void Delete(Guid id);

}
