using Flunt.Validations;

namespace Store.Domain.Entities;

public class Product : Entity
{
    public Product(string title, decimal price, bool active)
    {
        Title = title;
        Price = price;
        Active = active;

        AddNotifications
        (
            new Contract<Product>()
                .Requires()
                .IsNotEmpty(Title, "Title", "O título está inválido")
                .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que 0")
        );
    }

    public string Title { get; private set; }
    public decimal Price { get; private  set; }
    public bool Active { get; private  set; }
}