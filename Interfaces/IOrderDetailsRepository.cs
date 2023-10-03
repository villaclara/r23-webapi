using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IOrderDetailsRepository
	{
		ICollection<OrderDetails> GetOrderDetailsByOrderId(int orderId);
		Task<bool> AddOrderDetailsToOrderAsync(int orderId, OrderDetails orderDetails);
		Task<bool> UpdateOrderDetailsInOrderAsync(int orderId, OrderDetails orderDetails);
		Task<bool> RemoveOrderDetailsFromOrderAsync(int orderId, OrderDetails orderDetails);
		Task<bool> RemoveAllOrderDetailsByOrderId(int orderId);
	}
}
