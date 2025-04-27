namespace Domain.Entities;

public class Order : BaseEntity, IAuditEntity
{
    private readonly List<OrderItem> _orderItems = new();
    public Guid CustomerId { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; private set; }
    public DateTime? ModifiedOn { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public static Order Create(Guid customerId)
    {
        var order = new Order
        {
            CustomerId = customerId,
            Status = "Pending",
            CreatedOn = DateTime.UtcNow,
        };
        
        return order;
    }
    
    public void AddOrderItem(OrderItem orderItem)
        => _orderItems.Add(orderItem);

    public void RemoveOrderItems(Predicate<OrderItem> items)
        => _orderItems.RemoveAll(items);
    
    public void Update(string status, List<OrderItem> newItems)
    {
        Status = status;
        _orderItems.Clear();
        _orderItems.AddRange(newItems);
        ModifiedOn = DateTime.UtcNow;
    }
}