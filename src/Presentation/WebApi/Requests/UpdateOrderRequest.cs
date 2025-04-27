namespace WebApi.Requests;

public record UpdateOrderRequest(Guid CustomerId, string Status, List<UpdateOrderItemRequest> OrderItems);