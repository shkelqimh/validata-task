namespace Application.Responses;

public record OrderResponse(Guid Id, string Status, DateTime Created, DateTime? ModifiedOn, IReadOnlyList<OrderItemResponse> OrderItems);