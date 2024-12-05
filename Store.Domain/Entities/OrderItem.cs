using Flunt.Validations;

namespace Store.Domain.Entities;

public class OrderItem : Entity
{
    public OrderItem(Product product, int quantity)
    {        
        Product = product;
        Price = Product is not null ? Product.Price : 0;
        Quantity = quantity;
        
        AddNotifications
        (
            new Contract<OrderItem>()
                .Requires()
                .IsNotNull(Product, "Product", "O Produto não pode ser nulo")
                .IsGreaterThan(Quantity, 0, "Quantity", "A Quantidade deve ser maior que 0")
        );
    }

    public Product Product { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public decimal Total() => Price * Quantity;
}