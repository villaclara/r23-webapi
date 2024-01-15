using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Repository
{
	public class CandleItemRepository : ICandleItemRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public CandleItemRepository(ApplicationContext context)
		{
			_context = context;
		}

		public bool CandleExistsById(int candleId) =>
			_context.Candles.Any(c => c.Id == candleId);

		public bool CandleExistsByName(string candleName) =>
			_context.Candles.Any(c => c.Name == candleName);

		public async Task<bool> CreateCandleAsync(CandleItem candle)
		{
			_context.Candles.Add(candle);
			_context.CandleIngredients.Add(candle.Ingredient);
			return await SaveAsync();
		}

		public CandleItem? GetCandleById(int candleId) =>
			_context.Candles.Where(c => c.Id == candleId).Include(i => i.Ingredient).Include(c => c.Category).FirstOrDefault();


		public CandleItem? GetCandleByName(string candleName) 
		{
			// SQLite search not working with cyrillic letters and Trim() or ToLower()
			// so just first get all candles as list and then search in list using Trim and ToLower
			var candles = _context.Candles.Include(i => i.Ingredient).Include(c => c.Category).ToList();
			return candles.Where(c => c.Name.Trim().ToLower() == candleName.Trim().ToLower()).FirstOrDefault();
		}

		public IList<CandleItem> GetCandles() =>
			_context.Candles.OrderBy(c => c.Name).Include(i => i.Ingredient).Include(c => c.Category).ToList();

		public IEnumerable<CandleItem>? GetCandlesFromCategory(int categoryId) =>
			_context.Candles.Where(c => c.CategoryId == categoryId).OrderBy(c => c.Name)
				.Include(i => i.Ingredient).Include(c => c.Category).ToList() ?? default(IEnumerable<CandleItem>);

		public async Task<bool> RemoveCandleAsync(CandleItem candle)
		{
			_context.Candles.Remove(candle);
			//_context.CandleIngredients.Remove(candle.Ingredient);
			return await SaveAsync();
		}


		public async Task<bool> UpdateCandleAsync(CandleItem candle)
		{
            _context.Candles.Update(candle);
			return await SaveAsync();
		}


		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;

	}
}
