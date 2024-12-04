using Flunt.Validations;

namespace Store.Domain.Entities;

public class Product : Entity
{
    public Product(string title, decimal price, bool active)
    {
        AddNotifications
        (
            new Contract<Product>()
                .Requires()
                .IsNullOrEmpty(title, "Title", "O título está inválido")
                .IsLowerOrEqualsThan(price, 0, "Price", "O preço deve ser maior que 0")
        );

        Title = title;
        Price = price;
        Active = active;
    }

    public string Title { get; private set; }
    public decimal Price { get; private  set; }
    public bool Active { get; private  set; }
}