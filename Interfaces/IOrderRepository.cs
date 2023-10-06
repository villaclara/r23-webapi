using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IOrderRepository
	{
		ICollection<Order> GetOrders();
		Order? GetOrderById(int orderId);
		ICollection<Order> GetOrdersByCustomerId(int customerId);
		ICollection<Order> GetOrdersByDate(DateOnly date);
		ICollection<Order> GetOrdersByMinimalSum(int minimalSum);
		ICollection<Order> GetOrdersByMaximalSum(int maxSum);
		ICollection<Order> GetOrdersByPhoneNumber(string phoneNumber);
		Task<bool> CreateOrderAsync(Order order);
		Task<bool> UpdateOrderAsync(Order order);
		Task<bool> DeleteOrderAsync(Order order);
		bool OrderExistsById(int orderId);
	}
}
