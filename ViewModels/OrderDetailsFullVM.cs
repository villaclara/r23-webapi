using Road23.WebAPI.Models;

namespace Road23.WebAPI.ViewModels
{
	public record class OrderDetailsFullVM
	{
		public int CandleQuantity { get; set; }
		public int CandleId { get; set; }
	}
}
