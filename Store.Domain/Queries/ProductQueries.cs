using System.Linq.Expressions;
using Store.Domain.Entities;

namespace Store.Domain.Queries;

public class ProductQueries
{
    public static Expression<Func<Product, bool>> GetActiveProducts() => p => p.Active;

    public static Expression<Func<Product, bool>> GetInactiveProducts() => p => p.Active.Equals(false);
}