using Store.Domain.Entities;

namespace Store.Domain.Repositores;

public interface ICustomerRepository
{
    Customer Get(string document);
}