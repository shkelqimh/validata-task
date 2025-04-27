namespace Domain.Entities;

public class Customer : BaseEntity, IAuditEntity
{
    private readonly List<Order> _orders = new();

    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public DateTime CreatedOn { get; private set; }
    public DateTime? ModifiedOn { get; private set; }
    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    public static Customer Create(string firstName, string lastName, string address, string zipCode)
    {
        return new()
        {
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            ZipCode = zipCode,
            CreatedOn = DateTime.UtcNow,
        };
    }

    public void Update(string firstName, string lastName, string address, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        ZipCode = zipCode;
        ModifiedOn = DateTime.UtcNow;
    }
}