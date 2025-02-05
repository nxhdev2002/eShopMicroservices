namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler(
    ILogger<OrderUpdatedEventHandler> logger
) : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order with Id: {OrderId} is successfully updated.", notification.GetType().Name);
        throw new NotImplementedException();
    }
}
