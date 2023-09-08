namespace Road23.WebAPI.Models
{
	public class CandleIngredient
	{
		public int Id { get; set; }
		public int WaxNeededGram { get; set; }
		public int WickForDiameterCD { get; set; }
		public int CandleId { get; set; }
		public CandleItem Candle { get; set; } = null!;
	}
}
