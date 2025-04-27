namespace Application.Responses;

public record OrderItemResponse(Guid Id, Guid OrderId, Guid ProductId, int Quantity);