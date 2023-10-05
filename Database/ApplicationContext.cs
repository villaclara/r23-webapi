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

		public DbSet<CandleItem> Candles { get; set; }
		public DbSet<CandleCategory> CandleCategories { get; set; }
		public DbSet<CandleIngredient> CandleIngredients { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Receiver> Receivers { get; set; }
	}
}
