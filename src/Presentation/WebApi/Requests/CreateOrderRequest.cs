namespace WebApi.Requests;

public record CreateOrderRequest(Guid CustomerId, List<CreateOrderItemRequest> OrderItems);