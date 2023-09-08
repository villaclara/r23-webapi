namespace Road23.WebAPI.Models
{
	public class CandleItem
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int? BurningTimeMins { get; set; }
		public CandleCategory Category { get; set; } = null!;
		public CandleIngredient Ingredient { get; set; } = null!;

	}
}
