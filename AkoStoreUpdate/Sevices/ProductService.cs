using AkoStoreUpdate.Database;
using AkoStoreUpdate.Models;
using AkoStoreUpdate.Sevices.Abstract;

namespace AkoStoreUpdate.Sevices;

public class ProductService: BaseService, IProductService
{
    public ProductService(AkoStoreDatabase database) : base(database)
    {
    }
    public List<Product> GetAll()
    {
        return _database.Products;
    }
    public Product GetById(Guid id)
    {
        return _database.Products.FirstOrDefault(p => p.Id == id);

    }
    public void Add(Product item)
    {
        _database.Products.Add(item);
    }
    public void Update(Product item)
    {
        var product = GetById(item.Id);
        if (product != null)
        {
            product.Name = item.Name;
            product.Price = item.Price;
            product.Description = item.Description;
        }
    }

    public Product GetByBarcode(Guid barcode)
    {
        return _database.Products.FirstOrDefault(p => p.Barcode == barcode);
    }
    public void Delete(Guid id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _database.Products.Remove(product);
        }
    }
}
