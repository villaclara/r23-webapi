using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class OrderRepository : IOrderRepository
	{
		private ApplicationContext _context;
		public OrderRepository(ApplicationContext context)
		{
			_context = context;
		}
		public async Task<Order>? CreateOrderAsync(Order order)
		{
			_context.Orders.Add(order);
			foreach (var o in order.OrderDetails)
			{
				_context.OrderDetails.Add(o);
			}
			await _context.SaveChangesAsync();
			return order;
		}

		public Task<Order>? DeleteOrderAsync(Order order)
		{
			throw new NotImplementedException();
		}
		public Task<Order>? UpdateOrderAsync(Order order)
		{
			throw new NotImplementedException();
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
