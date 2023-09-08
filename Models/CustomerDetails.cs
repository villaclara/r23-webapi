namespace Road23.WebAPI.Models
{
	public class CustomerDetails
	{
		public int Id { get; set; }
		public string Password { get; set; } = null!;
		
		// customer related stuff
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = null!;
	}
}
