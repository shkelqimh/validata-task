namespace WebApi.Requests;

public record CreateCustomerRequest(string FirstName, string LastName, string Address, string ZipCode);