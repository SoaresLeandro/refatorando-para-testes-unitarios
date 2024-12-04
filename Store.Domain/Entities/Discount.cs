using Flunt.Validations;

namespace Store.Domain.Entities;

public class Discount : Entity
{
    public Discount(decimal amount, DateTime expireDate)
    {
        AddNotifications
        (
            new Contract<Discount>()
                .Requires()
                .IsLowerOrEqualsThan(amount, 0, "Amount", "O valor deve ser maior que 0")
                .IsLowerOrEqualsThan(expireDate, DateTime.Now, "ExpireDate", "A data de expiração não pode ser menor que a data atual")
        );

        Amount = amount;
        ExpireDate = expireDate;
    }

    public decimal Amount { get; private set; }
    public DateTime ExpireDate { get; private set; }


    public bool IsValid() => DateTime.Compare(DateTime.Now, ExpireDate) < 0;

    public decimal Value() => IsValid() ? Amount : 0;
}