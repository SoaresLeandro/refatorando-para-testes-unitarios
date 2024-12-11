using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class CreateOrderCommnad : Notifiable<Notification>, Icommand
{
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

    public string Customer { get; set; }
    public string ZipCode { get; set; }
    public string PromoCode { get; set; }
    public IList<CreateOrderItemCommand> Items { get; set; }

    public void Validate()
    {
        AddNotifications(
            new Contract<CreateOrderCommnad>()
            .Requires()
            .AreEquals(Customer.Length, 10, "Customer", "Cliente inválido")
            .AreEquals(ZipCode, 8, "ZipCode", "CEP inválido")
        );
    }
}
