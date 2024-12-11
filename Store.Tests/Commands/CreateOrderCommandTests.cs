using Store.Domain.Commands;

namespace Store.Tests.Commands;

[TestClass]
public class CreateOrderCommandTests
{
    [TestMethod]
    [TestCategory("Commands")]
    public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
    {
        var expected = false;

        var command = new CreateOrderCommnad();
        command.Customer = "";
        command.ZipCode = "123456789";
        command.PromoCode = "12345678";
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        command.Validate();

        var result = command.IsValid;

        Assert.AreEqual(expected, result);
    }
}