namespace Road23.WebAPI.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Email { get; set; } = null!;

		// customer details stuff
		public CustomerDetails CustomerDetails { get; set; } = null!;

		public ICollection<Order> Orders { get; set; } = null!;
	}
}
