using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IOrderDetailsRepository
	{
		ICollection<OrderDetails> GetOrderDetailsByOrderId(int orderId);
		Task<OrderDetails> AddOrderDetailsToOrderAsync(int orderId, OrderDetails orderDetails);
		OrderDetails UpdateOrderDetailsInOrderAsync(int orderId, OrderDetails orderDetails);
		ICollection<OrderDetails>? RemoveOrderDetailsFromOrderAsync(int orderId, OrderDetails orderDetails);
	}
}
