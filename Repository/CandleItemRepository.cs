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

		public async Task<CandleItem> CreateCandleAsync(CandleItem candle)
		{
			_context.Candles.Add(candle);
			_context.CandleIngredients.Add(candle.Ingredient);
			var saved = await SaveAsync();
			if(!saved)
				return new CandleItem();

			return candle;
		}

		public CandleItem? GetCandleById(int candleId) =>
			_context.Candles.Where(c => c.Id == candleId).Include(i => i.Ingredient).Include(c => c.Category).FirstOrDefault();

		public CandleItem? GetCandleByName(string candleName) =>
			_context.Candles.Where(c => c.Name.Trim().ToLower() == candleName.Trim().ToLower())
				.Include(i => i.Ingredient).Include(c => c.Category).FirstOrDefault();

		public IList<CandleItem> GetCandles() =>
			_context.Candles.OrderBy(c => c.Name).Include(i => i.Ingredient).Include(c => c.Category).ToList();

		public IEnumerable<CandleItem>? GetCandlesFromCategory(int categoryId) =>
			_context.Candles.Where(c => c.CategoryId == categoryId).OrderBy(c => c.Name)
				.Include(i => i.Ingredient).Include(c => c.Category).ToList();

		public async Task<CandleItem> RemoveCandleAsync(CandleItem candle)
		{
			_context.Candles.Remove(candle);
			//_context.CandleIngredients.Remove(candle.Ingredient);
			var saved = await SaveAsync();
			if (!saved)
				return new CandleItem();

			return candle;
		}


		public async Task<CandleItem> UpdateCandleAsync(CandleItem candle)
		{
            _context.Candles.Update(candle);
			var saved = await SaveAsync();
			if (!saved) 
				return new CandleItem();
			
			return candle;
		}


		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;

	}
}
