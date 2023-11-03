using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Road23.WebAPI.Models
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		
		[Precision(8, 2)]
		[Range(0, int.MaxValue)]
		public decimal TotalSum { get; set; }
		public string? Promocode { get; set; }
		public string? Comments { get; set; }
		public bool IsPaid { get; set; }
		public PaymentType PaymentType { get; set; }

		[Range(0, int.MaxValue)]
		public int ReceiverRepeat { get; set; }
		
		// Receiver stuff
		public Receiver Receiver { get; set; } = null!;

		// Customer stuff
		public int CustomerId { get; set; }
		public Customer Customer { get; set; } = null!;
		
		// OrderDetails 
		public ICollection<OrderDetails> OrderDetails { get; set; } = null!;

	}

	public enum PaymentType
	{
		Cash = 0,
		Card = 1,
		ZD = 2
	}
}
