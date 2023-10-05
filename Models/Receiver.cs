using Microsoft.Extensions.Logging.Abstractions;

namespace Road23.WebAPI.Models
{
	public class Receiver
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? FathersName { get; set; }
		public string PhoneNumber { get; set; } = null!;
		public string City { get; set; } = null!;
		public string DeliveryAdress { get; set; } = null!;

		public ICollection<Order> Orders { get; set; } = null!;
	}
}
