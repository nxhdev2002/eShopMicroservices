public class UpdateOrderHandler(IApplicationDbContext context) 
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await _context.Orders.FindAsync([orderId], cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }
        
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var updatedShipping = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
        var updatedBilling = Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.ShippingAddress.State, orderDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod);

        order.Update(
            OrderName.Of(orderDto.OrderName), 
            shippingAddress: updatedShipping,
            billingAddress: updatedBilling,
            payment: updatedPayment,
            status: orderDto.Status
        );
    }
}