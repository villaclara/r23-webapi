using Microsoft.EntityFrameworkCore;

namespace Road23.WebAPI.Models
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		
		[Precision(8, 2)]
		public decimal TotalSum { get; set; }
		public string? Promocode { get; set; }
		public string? Comments { get; set; }

		public int ReceiverRepeat { get; set; }
		
		// Receiver stuff
		public int ReceiverId { get; set; }
		public Receiver Receiver { get; set; } = null!;

		// Customer stuff
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = null!;
		
		// OrderDetails 
		public ICollection<OrderDetails> OrderDetails { get; set; } = null!;

	}
}
