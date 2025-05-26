using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;
using AkoStoreUpdate.Sevices.Abstract;

namespace AkoStoreUpdate.Sevices;

public class OrderService : BaseService, IOrderService
{
    public OrderService(AkoStoreDatabase database) : base(database)
    {
    }

    public void Add(Order item)
    {
        foreach (var product in item.Products)
        {
            item.TotalPrice += product.Price;
        }
        _database.Orders.Add(item);
    }

    public void Delete(Guid id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _database.Orders.Remove(product);
        }
    }

    public List<Order> GetAll()
    {
        return _database.Orders.ToList();
    }

    public Order GetById(Guid id)
    {
        return _database.Orders.FirstOrDefault(p => p.Id == id);
    }

    public void Update(Order item)
    {
        var order = GetById(item.Id);
        if (order != null)
        {
            order.Products = item.Products;
            order.TotalPrice = 0; 
            foreach (var product in item.Products)
            {
                order.TotalPrice += product.Price;
            }
        }
    }
}
