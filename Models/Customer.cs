namespace Road23.WebAPI.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;


		public ICollection<Order> Orders { get; set; } = null!;
	}
}
