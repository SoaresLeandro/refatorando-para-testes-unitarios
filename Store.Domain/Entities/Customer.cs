using Flunt.Validations;

namespace Store.Domain.Entities;

public class Customer : Entity
{
    public Customer(string name, string email)
    {
        AddNotifications
        (
            new Contract<Customer>()
                .Requires()
                .IsNullOrEmpty(name, "Name", "O nome está inválido")
                .IsNullOrEmpty(email, "Email", "O e-mail está inválido")
        );

        Name = name;
        Email = email;
    }

    public string Name { get; private set; }    
    public string Email { get; private set; }
}