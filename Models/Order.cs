namespace Road23.WebAPI.Models
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalSum { get; set; }
		public string? Promocode { get; set; }
		public ICollection<OrderDetails> OrderDetails { get; set; } = null!;

	}
}
