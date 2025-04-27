namespace Domain.Entities;

public class Product : BaseEntity, IAuditEntity
{
    public string Name { get; private set; } = string.Empty;
    public double Price { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? ModifiedOn { get; private set; }
    
    public static Product Create(string name, double price)
        => new () { Name = name, Price = price, CreatedOn = DateTime.UtcNow };

    public void Update(string name, double price)
    {
        Name = name;
        Price = price;
        ModifiedOn = DateTime.UtcNow;
    }
}