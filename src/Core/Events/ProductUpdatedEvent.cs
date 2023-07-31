namespace Core.Events
{
    public record ProductUpdatedEvent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Sku { get; init; } = string.Empty;
        public DateTime UpdatedAt { get; init; }
    }
}
