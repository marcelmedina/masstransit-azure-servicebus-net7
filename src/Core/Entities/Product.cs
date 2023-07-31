using Core.Events;

namespace Core.Entities
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Sku { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }

        public ProductCreatedEvent ToProductCreatedEvent()  {
            return new ProductCreatedEvent
            {
                Id = Id,
                Name = Name,
                Sku = Sku,
                CreatedAt = CreatedAt
            };
        }

        public ProductUpdatedEvent ToProductUpdatedEvent()
        {
            return new ProductUpdatedEvent()
            {
                Id = Id,
                Name = Name,
                Sku = Sku,
                UpdatedAt = UpdatedAt
            };
        }
    }
}
