using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities;

public class Order : Entity
{
    public Order(Customer customer,decimal deliveryFee, Discount discount)
    {
        AddNotifications
        (
            new Contract<Order>()
                .Requires()
                .IsNotNull(customer, "Customer", "O Cliente não pode ser nulo")
        );

        Customer = customer;
        Date = DateTime.Now;
        Number = Guid.NewGuid().ToString().Substring(0, 8);
        DeliveryFee = deliveryFee;
        Discount = discount;
        Status = EOrderStatus.WaitingPayment;
        Items = new List<OrderItem>();
    }

    public Customer Customer { get; set; }
    public DateTime Date { get; set; }
    public string Number { get; set; }
    public IList<OrderItem> Items { get; set; }
    public decimal DeliveryFee { get; set; }
    public Discount Discount { get; set; }
    public EOrderStatus Status { get; set; }

    public void AddItem(Product product, int quantity)
    {
        var item = new OrderItem(product, quantity);

        if(item.IsValid)
            Items.Add(item);
    }

    public decimal Total()
    {
        decimal total = 0;

        foreach(var item in Items)
            total += item.Total();

        total += DeliveryFee;
        total -= Discount is not null ? Discount.Value() : 0;

        return total;
    }

    public void Pay(decimal amount)
    {
        if(amount == Total())
            Status = EOrderStatus.WaitingDelivery;
    }

    public void Cancel() => Status = EOrderStatus.Canceled;
}