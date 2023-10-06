using Microsoft.EntityFrameworkCore;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Database
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options) 
		{ 
		
		}

		public DbSet<CandleItem> Candles { get; set; } = null!;
		public DbSet<CandleCategory> CandleCategories { get; set; } = null!;
		public DbSet<CandleIngredient> CandleIngredients { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public DbSet<OrderDetails> OrderDetails { get; set; } = null!;
		public DbSet<Customer> Customers { get; set; } = null!;
		public DbSet<Receiver> Receivers { get; set; } = null!;
	}
}
