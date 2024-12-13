using Store.Domain.Entities;
using Store.Domain.Repositores;

namespace Store.Tests.Repositories;

public class FakeOrderRepository : IOrderRepository
{
    public void Save(Order order)
    {
    }
}
