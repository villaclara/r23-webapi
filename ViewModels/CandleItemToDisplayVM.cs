namespace Road23.WebAPI.ViewModels
{
	public class CandleItemToDisplayVM
	{
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string Category { get; set; } = null!;
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int? BurningTimeMins { get; set; }
		public int WaxNeededGram { get; set; }
		public int WickDiameterCM { get; set; }
	}
}
