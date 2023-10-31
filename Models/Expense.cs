using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Road23.WebAPI.Models
{
	public class Expense
	{
		public int Id { get; set; }
		public string Description { get; set; } = null!;
		public DateTime Date { get; set; }

		[Precision(8, 2)]
		[Range(0, int.MaxValue)]
		public decimal Cost {  get; set; }
	}
}
