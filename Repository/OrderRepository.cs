using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationContext _context;
		public OrderRepository(ApplicationContext context)
		{
			_context = context;
		}
		public async Task<bool> CreateOrderAsync(Order order)
		{
			// Order and OrderDetails are automatically added together, no need to add explicitly OrderDetails
			_context.Orders.Add(order);
			//_context.OrderDetails.AddRange(order.OrderDetails);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<Order> DeleteOrderAsync(Order order)
		{
			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();
			return order;
		}
		public async Task<Order> UpdateOrderAsync(Order order)
		{
			_context.Orders.Update(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public Order? GetOrderById(int orderId) =>
			_context.Orders.Where(delegate(Order o) { return o.Id ==  orderId; }).FirstOrDefault();

		public ICollection<Order> GetOrders() =>
			_context.Orders.ToList();

		public ICollection<Order>? GetOrdersByCustomerId(int customerId) =>
			_context.Orders.Where(o => o.CustomerId == customerId).ToList();

		public ICollection<Order>? GetOrdersByDate(DateOnly date) =>
			_context.Orders.Where(o => o.OrderDate.Date == date.ToDateTime(new TimeOnly()).Date).ToList();

		public ICollection<Order>? GetOrdersByMaximalSum(int maxSum) =>
			_context.Orders.Where(o => o.TotalSum <= maxSum).ToList();

		public ICollection<Order>? GetOrdersByMinimalSum(int minimalSum) =>
			_context.Orders.Where(o => o.TotalSum >= minimalSum).ToList();

		public bool OrderExistsById(int orderId) =>
			_context.Orders.Any(o => o.Id == orderId);

	}
}
