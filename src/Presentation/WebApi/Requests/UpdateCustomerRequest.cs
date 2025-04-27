namespace WebApi.Requests;

public record UpdateCustomerRequest(string FirstName, string LastName, string Address, string ZipCode);