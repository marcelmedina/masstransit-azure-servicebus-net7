namespace Core.Events
{
    public record ProductCreatedEvent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Sku { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
