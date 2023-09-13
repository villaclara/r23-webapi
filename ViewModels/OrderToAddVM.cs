using Road23.WebAPI.Models;

namespace Road23.WebAPI.ViewModels
{
	public class OrderToAddVM
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalSum { get; set; }
		public string? Promocode { get; set; }
		public int CustomerId { get; set; }
		public ICollection<OrderDetailsToAddVM> OrderDetails { get; set; } = null!;
	}
}
