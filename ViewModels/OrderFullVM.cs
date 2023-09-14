using Road23.WebAPI.Models;

namespace Road23.WebAPI.ViewModels
{
	public class OrderFullVM
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalSum { get; set; }
		public string? Promocode { get; set; }
		public string? Comments { get; set; }
		public string? Receiver { get; set; }
		public int CustomerId { get; set; }
		public ICollection<OrderDetailsFullVM> OrderDetails { get; set; } = null!;

	}
}
