using Flunt.Validations;

namespace Store.Domain.Entities;

public class Discount : Entity
{
    public Discount(decimal amount, DateTime expireDate)
    {
        Amount = amount;
        ExpireDate = expireDate;

        AddNotifications
        (
            new Contract<Discount>()
                .Requires()
                .IsGreaterThan(Amount, 0, "Amount", "O valor deve ser maior que 0")
        );
    }

    public decimal Amount { get; private set; }
    public DateTime ExpireDate { get; private set; }


    public bool IsValid() => DateTime.Compare(DateTime.Now, ExpireDate) < 0;

    public decimal Value() => IsValid() ? Amount : 0;
}