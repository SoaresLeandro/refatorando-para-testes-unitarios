using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Customer _customer = new Customer("Customer", "customer@email.com");
    private readonly Product _product = new Product("Produto 01", 19, true);
    private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(10));

    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoValidoDeveRetornarUmNumeroComOitoCaracteres()
    {
        var order = new Order(_customer, 0, null);

        Assert.AreEqual(8, Convert.ToInt32(order.Number.Length));
    }

    public void DadoUmPedidoValidoSeuStatusDeveSerAguardandoPagamento()
    {
        var order = new Order(_customer, 0, null);

        Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
    }
}