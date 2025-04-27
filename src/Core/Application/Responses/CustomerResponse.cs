namespace Application.Responses;

public record CustomerResponse(Guid Id, string FirstName, string LastName, string Address, string ZipCode);