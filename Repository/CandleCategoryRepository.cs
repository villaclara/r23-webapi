using Microsoft.EntityFrameworkCore;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Repository
{
	public class CandleCategoryRepository : ICandleCategoryRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public CandleCategoryRepository(ApplicationContext context)
		{
			_context = context;
		}

		public IList<CandleCategory> GetCategories() =>
			_context.CandleCategories.OrderBy(c => c.Id).Include(c => c.Candles).ToList();

		public CandleCategory? GetCategoryById(int categoryId) => 
			_context.CandleCategories.Where(c => c.Id == categoryId).Include(c => c.Candles).FirstOrDefault();

		public CandleCategory? GetCategoryByName(string name) =>
			_context.CandleCategories.Where(c => c.Name == name).Include(c => c.Candles).FirstOrDefault();

		public async Task<CandleCategory> CreateCategoryAsync(CandleCategory candleCategory)
		{
			_context.CandleCategories.Add(candleCategory);
			var saved = await SaveAsync();
			if (!saved)
				return new CandleCategory();

			return candleCategory;
		}

		public async Task<bool> RemoveCategoryAsync(CandleCategory candleCategory) 
		{
			_context.Remove(candleCategory);
			return await SaveAsync();
		}

		public bool CategoryExistsByName(string categoryName) => 
			_context.CandleCategories.Any(c => c.Name.Trim().ToLower() == categoryName.Trim().ToLower());

		public async Task<CandleCategory> UpdateCategoryAsync(CandleCategory candleCategory)
		{
			_context.Update(candleCategory);
			var saved = await SaveAsync();
			if (!saved) 
				return new CandleCategory();
			
			return candleCategory;
		}

		public bool CategoryExistsById(int categoryId) =>
			_context.CandleCategories.Any(c => c.Id == categoryId);

		public bool CandlesExistInCategoryId(int categoryId) =>
			_context.Candles.Any(c => c.CategoryId == categoryId);


		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;
	}
}
