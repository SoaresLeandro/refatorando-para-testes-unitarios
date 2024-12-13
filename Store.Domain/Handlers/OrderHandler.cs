using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositores;
using Store.Domain.Utils;

namespace Store.Domain.Handlers;

public class OrderHandler : Notifiable<Notification>, IHandler<CreateOrderCommnad>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(
        ICustomerRepository customerRepository, 
        IDeliveryFeeRepository deliveryFeeRepository, 
        IDiscountRepository discountRepository, 
        IProductRepository productRepository, 
        IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _deliveryFeeRepository = deliveryFeeRepository;
        _discountRepository = discountRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public ICommandResult Handle(CreateOrderCommnad command)
    {
        //Fail Fast Validation
        if(!command.IsValid)
            return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

        //Recupera o cliente
        var customer = _customerRepository.Get(command.Customer);

        //Calcula o frete
        var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

        //Obtén o cupom de desconto
        var discount = _discountRepository.Get(command.PromoCode);

        //Gera o pedido
        var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
        var order = new Order(customer, deliveryFee, discount);

        foreach(var item in command.Items)
        {
            var product = products.Where(p => p.Id == item.Product).FirstOrDefault();
            order.AddItem(product, item.Quantity);
        }

        //Agrupa as notificações
        AddNotifications(order.Notifications);

        if(!IsValid)
            return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

        _orderRepository.Save(order);
            return new GenericCommandResult(true, "Pedido criado com sucesso!", null);
    }
}
