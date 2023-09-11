using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Repository
{
	public class CandleCategoryRepository : ICandleCategoryRepository
	{
		private readonly ApplicationContext _context;
		public CandleCategoryRepository(ApplicationContext context)
		{
			_context = context;
		}

		public IList<CandleCategory> GetCategories() =>
			_context.CandleCategories.ToList();

		public CandleCategory? GetCategoryById(int categoryId) => 
			_context.CandleCategories.Where(c => c.Id == categoryId).FirstOrDefault();

		public CandleCategory? GetCategoryByName(string name) =>
			_context.CandleCategories.Where(c => c.Name == name).FirstOrDefault();

		public async Task<CandleCategory> CreateCategoryAsync(CandleCategory candleCategory)
		{
			_context.CandleCategories.Add(candleCategory);
			await _context.SaveChangesAsync();
			return candleCategory;
		}

		public async Task<bool> RemoveCategoryAsync(CandleCategory candleCategory) 
		{
			_context.Remove(candleCategory);
			return await _context.SaveChangesAsync() > 0;
		}

		public bool CategoryExistsByName(string categoryName) => 
			_context.CandleCategories.Any(c => c.Name.Trim().ToLower() == categoryName.Trim().ToLower());

		public async Task<CandleCategory> UpdateCategoryAsync(CandleCategory candleCategory)
		{
			_context.Update(candleCategory);
			await _context.SaveChangesAsync();
			return candleCategory;
		}

		public bool CategoryExistsById(int categoryId) =>
			_context.CandleCategories.Any(c => c.Id == categoryId);

		public bool CandlesExistInCategoryId(int categoryId) =>
			_context.Candles.Any(c => c.CategoryId == categoryId);
		
	}
}
