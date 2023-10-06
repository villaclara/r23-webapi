using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Road23.WebAPI.Models
{
	public class Receiver
	{
		public int Id { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? FathersName { get; set; }
		
		[StringLength(11)]
		public string PhoneNumber { get; set; } = null!;
		public string City { get; set; } = null!;
		public string DeliveryAdress { get; set; } = null!;

		// order related stuff
		public int OrderId { get; set; }
		public Order Order { get; set; } = null!;
	}
}
