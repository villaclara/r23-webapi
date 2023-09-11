namespace Road23.WebAPI.ViewModels
{
	public class CandleCategoryFullVM
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<string>? Candles { get; set; } = null!;
	}
}
