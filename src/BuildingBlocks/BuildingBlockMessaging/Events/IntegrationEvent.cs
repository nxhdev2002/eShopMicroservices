namespace BuildingBlockMessaging.Events;
public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime? OccurredOn => DateTime.Now;
    public string? EventName => GetType().AssemblyQualifiedName;
}
