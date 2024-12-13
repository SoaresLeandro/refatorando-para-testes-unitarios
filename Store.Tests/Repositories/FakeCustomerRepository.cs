using Store.Domain.Entities;
using Store.Domain.Repositores;

namespace Store.Tests.Repositories;

public class FakeCustomerRepository : ICustomerRepository
{
    public Customer Get(string document)
    {
        if(document.Equals("1234567891"))
            return new Customer("Customer", "customer@email.com");

        return null;
    }
}