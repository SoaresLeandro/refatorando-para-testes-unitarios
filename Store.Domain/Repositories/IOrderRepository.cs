using Store.Domain.Entities;

namespace Store.Domain.Repositores;

public interface IOrderRepository
{
    void Save(Order order);
}