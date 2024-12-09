using Store.Domain.Entities;

namespace Store.Domain.Repositores;

public interface IDiscountRepository
{
    Discount Get(string code);
}