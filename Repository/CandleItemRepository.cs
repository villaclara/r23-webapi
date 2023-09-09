using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Repository
{
	public class CandleItemRepository : ICandleItemRepository
	{
		private readonly ApplicationContext _context;
		public CandleItemRepository(ApplicationContext context)
		{
			_context = context;
		}


		public bool CandleExistsByName(string candleName) =>
			_context.Candles.Any(c => c.Name == candleName);

		public async Task<CandleItem> CreateCandleAsync(CandleItem candle)
		{
			_context.Candles.Add(candle);
			_context.CandleIngredients.Add(candle.Ingredient);
			await _context.SaveChangesAsync();
			return candle;
		}

		public CandleItem? GetCandleById(int candleId)
		{
			var cndl = _context.Candles.Where(c => c.Id == candleId).FirstOrDefault();

			
			var ing = _context.CandleIngredients.Where(i => i.CandleId == candleId).FirstOrDefault();

			if (cndl is not null && ing is not null)
				cndl.Ingredient = ing;
			return cndl;
		}

		public CandleItem? GetCandleByName(string candleName) =>
			_context.Candles.Where(c => c.Name.Trim().ToLower() == candleName.Trim().ToLower()).FirstOrDefault();

		public IList<CandleItem> GetCandles() =>
			_context.Candles.OrderBy(c => c.Name).ToList();

		public async Task<CandleItem> RemoveCandle(CandleItem candle)
		{
			_context.Candles.Remove(candle);
			_context.CandleIngredients.Remove(candle.Ingredient);
			await _context.SaveChangesAsync();
			return candle;
		}

		public async Task<CandleItem> UpdateCandle(CandleItem candle)
		{
			_context.Update(candle);
			await _context.SaveChangesAsync();
			return candle;
		}
	}
}
