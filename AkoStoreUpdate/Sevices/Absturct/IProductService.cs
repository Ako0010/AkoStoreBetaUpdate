using AkoStoreUpdate.Models;

namespace AkoStoreUpdate.Sevices.Abstract;

public interface IProductService
{
    public List<Product> GetAll();
    public Product GetById(Guid id);
    public void Add(Product item);
    public void Update(Product item);
    public void Delete(Guid id);
    public Product GetByBarcode(Guid barcode);
}
