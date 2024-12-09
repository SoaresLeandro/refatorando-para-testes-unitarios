using Store.Domain.Repositores;

namespace Store.Tests.Repositories;

public class FakeDeliveryFeeRepository : IDeliveryFeeRepository
{
    public decimal Get(string zipCode) => 10;
}