using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class OrderDetailsRepository : IOrderDetailsRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public OrderDetailsRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<bool> AddOrderDetailsToOrderAsync(int orderId, OrderDetails orderDetails)
		{
			_context.OrderDetails.Add(orderDetails);
			return await SaveAsync();
		}

		public ICollection<OrderDetails> GetOrderDetailsByOrderId(int orderId) =>
			_context.OrderDetails.Where(o =>  o.OrderId == orderId).ToList();

		public async Task<bool> RemoveAllOrderDetailsByOrderId(int orderId)
		{
			var details = _context.OrderDetails.Where(d => d.OrderId == orderId);
			if (details.Any())
			{
				_context.RemoveRange(details);
				var saved = await SaveAsync();
				if(saved)
					return true;
			}

			return false;
		}

		public async Task<bool> RemoveOrderDetailsFromOrderAsync(int orderId, OrderDetails orderDetails)
		{
			_context.OrderDetails.Remove(orderDetails);
			return await SaveAsync();
		}

		public async Task<bool> UpdateOrderDetailsInOrderAsync(int orderId, OrderDetails orderDetails)
		{
			_context.OrderDetails.Update(orderDetails);
			return await SaveAsync();
		}

		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;
	}
}
