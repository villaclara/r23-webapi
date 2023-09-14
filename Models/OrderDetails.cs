namespace Road23.WebAPI.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public int CandleQuantity { get; set; }
		
		// candle related stuff
		public int CandleId { get; set; }
		public CandleItem Candle { get; set; } = null!;

		// order related stuff
		public int OrderId { get; set; }
		public Order? Order { get; set; }
	}
}
