namespace Store.Domain.Repositores;

public interface IDeliveryFeeRepository
{
    decimal Get(string zipCode);
}