namespace Road23.WebAPI.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Candle> Candles { get; set; }
	}
}
