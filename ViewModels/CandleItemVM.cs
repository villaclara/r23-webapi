using Road23.WebAPI.Models;


// NOT USED FOR NOW

// maybe will be used later in adding candle
namespace Road23.WebAPI.ViewModels
{
	public class CandleItemVM
	{
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int? BurningTimeMins { get; set; }
		public int WaxNeededGram { get; set; }
		public int WickDiameterCM { get; set; }

		// categoryID is from query
		// public int CategoryId { get; set; }
	}
}
