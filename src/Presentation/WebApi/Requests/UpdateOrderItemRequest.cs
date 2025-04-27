namespace WebApi.Requests;

public record UpdateOrderItemRequest(Guid Id, Guid ProductId, int Quantity);