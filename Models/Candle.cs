namespace Road23.WebAPI.Models
{
	public class Candle
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int? BurningTimeMins { get; set; }
		public Category Category { get; set; }
		public CandleIngredient Ingredient { get; set; }

	}
}
