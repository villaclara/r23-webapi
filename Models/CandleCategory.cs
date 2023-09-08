namespace Road23.WebAPI.Models
{
	public class CandleCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public ICollection<CandleItem> Candles { get; set; } = null!;
	}
}
