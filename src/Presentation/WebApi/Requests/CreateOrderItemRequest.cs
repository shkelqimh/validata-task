namespace WebApi.Requests;

public record CreateOrderItemRequest(Guid ProductId, int Quantity);