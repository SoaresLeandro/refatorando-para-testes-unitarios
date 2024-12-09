using Store.Domain.Entities;

namespace Store.Domain.Repositores;

public interface IProductRepository
{
    IEnumerable<Product> Get(IEnumerable<Guid> ids);
}