namespace Domain.Entities;

public class OrderItem : BaseEntity, IAuditEntity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? ModifiedOn { get; private set; }
    public Product Product { get; private set; } = null!;
    public Order Order { get; private set; } = null!;

    public static OrderItem Create(Guid orderId, Guid productId, int quantity)
    {
        return new ()
        {
            OrderId = orderId,
            ProductId = productId,
            CreatedOn = DateTime.UtcNow,
            Quantity = quantity
        };
    }
    
    public static OrderItem CreateWithoutId(Guid orderId, Guid productId, int quantity)
    {
        return new ()
        {
            Id = default,
            OrderId = orderId,
            ProductId = productId,
            CreatedOn = DateTime.UtcNow,
            Quantity = quantity
        };
    }

    public void Update(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
        ModifiedOn = DateTime.UtcNow;
    }
}