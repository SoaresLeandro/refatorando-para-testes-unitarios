using Flunt.Validations;

namespace Store.Domain.Entities;

public class Customer : Entity
{
    public Customer(string name, string email)
    {
        Name = name;
        Email = email;

        AddNotifications
        (
            new Contract<Customer>()
                .Requires()
                .IsNotEmpty(Name, "Name", "O nome está inválido")
                .IsNotEmpty(Email, "Email", "O e-mail está inválido")
        );
    }

    public string Name { get; private set; }    
    public string Email { get; private set; }
}