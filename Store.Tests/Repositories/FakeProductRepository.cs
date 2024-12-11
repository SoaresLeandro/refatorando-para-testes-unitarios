using Store.Domain.Entities;
using Store.Domain.Repositores;

namespace Store.Tests.Repositories;

public class FakeProductRepository : IProductRepository
{
    public IList<Product> _products;

    public FakeProductRepository()
    {        
        _products = new List<Product>();
        _products.Add(new Product("Produto 01", 10, true));
        _products.Add(new Product("Produto 02", 10, true));
        _products.Add(new Product("Produto 03", 10, false));
        _products.Add(new Product("Produto 04", 10, false));
        _products.Add(new Product("Produto 05", 10, true));
    }

    public IEnumerable<Product> Get(IEnumerable<Guid> ids) => _products;

    public IEnumerable<Product> GetAll() => _products;
}
