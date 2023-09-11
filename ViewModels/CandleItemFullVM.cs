using Microsoft.EntityFrameworkCore;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.ViewModels
{
	// full info about CandleItem + Ingredient + Category
	public class CandleItemFullVM
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string Category { get; set; } = null!;
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int HeightCM { get; set; }
		public int? BurningTimeMins { get; set; }
		public int WaxNeededGram { get; set; }
		public int WickDiameterCM { get; set; }
	}
}
