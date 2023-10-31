namespace Road23.WebAPI.ViewModels
{
	// basic ViewModel of CandleItem + Ingredients + Category Name
	// used to display for basic users (NOT ADMIN)
	public record class CandleItemBasicVM
	{
		public string Name { get; set; } = null!;
		public string? Desciption { get; set; }
		public string Category { get; set; } = null!;
		public decimal Price { get; set; }
		public int Height { get; set; }
		public int? BurningTime { get; set; }

	}
}
