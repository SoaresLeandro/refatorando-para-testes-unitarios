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

    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoValidoSeuStatusDeveSerAguardandoPagamento()
    {
        var order = new Order(_customer, 0, null);

        Assert.AreEqual(EOrderStatus.WaitingPayment, order.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPagamentoValidoOStatusDoPedidoDeveSerAguardandoEntrega()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 2);

        var total = order.Total();
        order.Pay(total);

        Assert.AreEqual(EOrderStatus.WaitingDelivery, order.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmPedidoCanceladoDeveRetornarStatusCancelado()
    {
        var order = new Order(_customer, 0, null);

        order.Cancel();

        Assert.AreEqual(EOrderStatus.Canceled, order.Status);
    }

    [TestMethod]
    [TestCategory("Domain")]
    public void DadoUmNovoItemSemProdutoOMesmoNaoDeveSerAdicionado()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(null, 1);

        Assert.AreEqual(0, order.Items.Count);
    }
}