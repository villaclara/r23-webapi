using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class CandleCategoryRepository : ICandleCategoryRepository
	{
		private readonly ApplicationContext _context;
		public CandleCategoryRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<CandleCategory> CreateCategory(CandleCategory candleCategory)
		{
			_context.CandleCategories.Add(candleCategory);
			await _context.SaveChangesAsync();
			return candleCategory;
		}

		public IList<CandleCategory> GetCategories()
		{
			return _context.CandleCategories.ToList();
		}

		public CandleCategory? GetCategoryById(int categoryId)
		{
			return _context.CandleCategories.Where(c =>  c.Id == categoryId).FirstOrDefault();
		}

		public async Task<CandleCategory> RemoveCategory(CandleCategory candleCategory)
		{
			_context.Remove(candleCategory);
			await _context.SaveChangesAsync();
			return candleCategory;
		}
	}
}
