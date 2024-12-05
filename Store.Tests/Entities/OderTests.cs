using Store.Domain.Entities;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoValidoDeveRetornarUmNumeroComOitoCaracteres()
    {
        var customer = new Customer("Customer", "customer@email.com");
        // Discount discount = new Discount(15000, DateTime.Now.AddDays(2));
        var order = new Order(customer, 0, null);

        Assert.AreEqual(8, Convert.ToInt32(order.Number.Length));
    }
}