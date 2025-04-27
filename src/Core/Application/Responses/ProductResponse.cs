namespace Application.Responses;

public record ProductResponse(Guid Id, string Name, double Price, DateTime CreatedOn, DateTime? ModifiedOn);