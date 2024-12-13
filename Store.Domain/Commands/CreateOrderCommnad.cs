using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderCommnad : Notifiable<Notification>, ICommand
{
    public string Customer { get; set; }
    public string ZipCode { get; set; }
    public string PromoCode { get; set; }
    public IList<CreateOrderItemCommand> Items { get; set; }
    
    public CreateOrderCommnad()
    {
        Items = new List<CreateOrderItemCommand>();
    }

    public CreateOrderCommnad(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
    {
        Customer = customer;
        ZipCode = zipCode;
        PromoCode = promoCode;
        Items = items;
    }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateOrderCommnad>()
            .Requires()
            .IsNotNullOrEmpty(Customer, "Customer", "Informe o cliente")
            .AreEquals(Customer?.Length, 10, "Customer", "Cliente inválido")
            .AreEquals(ZipCode, 8, "ZipCode", "CEP inválido")
            .IsGreaterOrEqualsThan(Items, 1, "Items", "O pedido precisa conter ao menos 1 item")
        );
    }
}
