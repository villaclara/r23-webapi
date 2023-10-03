using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Road23.WebAPI.Repository
{
	public class OrderRepository : IOrderRepository, IContextSave
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
			return await SaveAsync();
		}

		public async Task<bool> DeleteOrderAsync(Order order)
		{
			_context.Orders.Remove(order);
			return await SaveAsync();
		}
		public async Task<bool> UpdateOrderAsync(Order order)
		{
			//_context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			//foreach (var details in order.OrderDetails)
			//{
			//	_context.Entry(details).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			//}

			_context.Orders.Update(order);
			return await SaveAsync();
		}

		public Order? GetOrderById(int orderId) =>
			_context.Orders.Where(o => o.Id == orderId).Include(o => o.Customer).Include(o => o.OrderDetails).FirstOrDefault();

		public ICollection<Order> GetOrders() =>
			_context.Orders.OrderBy(o => o.OrderDate).Include(o => o.Customer).Include(o => o.OrderDetails).ToList();

		public ICollection<Order> GetOrdersByCustomerId(int customerId) =>
			_context.Orders.Where(o => o.CustomerId == customerId).Include(o => o.Customer).Include(o => o.OrderDetails).ToList();

		public ICollection<Order> GetOrdersByDate(DateOnly date) =>
			_context.Orders.Where(o => o.OrderDate.Date == date.ToDateTime(new TimeOnly()).Date)
				.Include(o => o.Customer).Include(o => o.OrderDetails).ToList();

		public ICollection<Order> GetOrdersByMaximalSum(int maxSum) =>
			_context.Orders.Where(o => o.TotalSum <= maxSum).Include(o => o.Customer).Include(o => o.OrderDetails).ToList();

		public ICollection<Order> GetOrdersByMinimalSum(int minimalSum) =>
			_context.Orders.Where(o => o.TotalSum >= minimalSum).Include(o => o.Customer).Include(o => o.OrderDetails).ToList();

		public bool OrderExistsById(int orderId) =>
			_context.Orders.Any(o => o.Id == orderId);



		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;
	}
}
