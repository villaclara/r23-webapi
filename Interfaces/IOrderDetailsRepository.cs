using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IOrderDetailsRepository
	{
		ICollection<OrderDetails> GetOrderDetailsByOrderId(int orderId);
		Task<OrderDetails> AddOrderDetailsToOrderAsync(int orderId, OrderDetails orderDetails);
		Task<OrderDetails> UpdateOrderDetailsInOrderAsync(int orderId, OrderDetails orderDetails);
		Task<OrderDetails> RemoveOrderDetailsFromOrderAsync(int orderId, OrderDetails orderDetails);
		Task<bool> RemoveAllOrderDetailsByOrderId(int orderId);
	}
}
