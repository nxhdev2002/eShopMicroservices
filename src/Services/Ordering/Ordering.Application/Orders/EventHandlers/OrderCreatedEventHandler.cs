namespace Ordering.Application.Orders.EventHandlers;
public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order with Id: {OrderId} is successfully created.", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
