using System.Xml.Serialization;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers;
using Store.Domain.Repositores;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers;

[TestClass]
public class OrderHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandlerTests()
    {
        _customerRepository = new FakeCustomerRepository();
        _deliveryFeeRepository = new FakeDeliveryFeeRepository();
        _discountRepository = new FakeDiscountRepository();
        _productRepository = new FakeProductRepository();
        _orderRepository = new FakeOrderRepository();
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
    {
        bool expected = false;

        CreateOrderCommnad orderCommand = new CreateOrderCommnad();
        orderCommand.Customer = null;
        orderCommand.PromoCode = "10";
        orderCommand.ZipCode = "12345678";
        orderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        orderCommand.Validate();

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(orderCommand);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmCEPInvalidoOPedidoNaoDeveSerGerado()
    {
        bool expected = false;
        CreateOrderCommnad createOrderCommnad = new CreateOrderCommnad();
        createOrderCommnad.Customer = "1234567891";
        createOrderCommnad.PromoCode = "10";
        createOrderCommnad.ZipCode = "12345";
        createOrderCommnad.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        createOrderCommnad.Validate();

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(createOrderCommnad);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmPromoCodeInexistenteOPedidoDeveSerGeradoNormalmente()
    {
        bool expected = true;
        CreateOrderCommnad createOrderCommnad = new CreateOrderCommnad();
        createOrderCommnad.Customer = "1234567891";
        createOrderCommnad.PromoCode = "22";
        createOrderCommnad.ZipCode = "12345678";
        createOrderCommnad.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        createOrderCommnad.Validate();

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(createOrderCommnad);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmPedidoSemItensOMesmoNaoDeveSerGerado()
    {
        bool expected = false;
        CreateOrderCommnad createOrderCommnad = new CreateOrderCommnad();
        createOrderCommnad.Customer = "1234567891";
        createOrderCommnad.PromoCode = null;
        createOrderCommnad.ZipCode = "12345678";

        createOrderCommnad.Validate();

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(createOrderCommnad);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
    {
        bool expected = false;

        CreateOrderCommnad createOrderCommnad = new CreateOrderCommnad();
        createOrderCommnad.Customer = null;
        createOrderCommnad.PromoCode = "10";
        createOrderCommnad.ZipCode = "12345678";
        createOrderCommnad.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        createOrderCommnad.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));

        createOrderCommnad.Validate();

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(createOrderCommnad);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void DadoUmComandoValidoOPedidoDeveSerGerado()
    {
        bool expected = true;

        CreateOrderCommnad orderCommand = new CreateOrderCommnad();
        orderCommand.Customer = "1234567891";
        orderCommand.PromoCode = "1235";
        orderCommand.ZipCode = "12345678";
        orderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));
        orderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 4));
        orderCommand.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

        OrderHandler orderHandler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
        
        GenericCommandResult commandResult = (GenericCommandResult)orderHandler.Handle(orderCommand);

        bool result = commandResult.Success;

        Assert.AreEqual(expected, result);
    }
}