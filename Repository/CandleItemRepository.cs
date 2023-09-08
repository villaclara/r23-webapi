using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class CandleItemRepository : ICandleItemRepository
	{
		private readonly ApplicationContext _context;
		public CandleItemRepository(ApplicationContext context)
		{
			_context = context;
		}


		public bool CandleExists(CandleItem candle) =>
			GetCandleById(candle.Id) != null || GetCandleByName(candle.Name) != null;

		public async Task<CandleItem> CreateCandle(CandleItem candle)
		{
			_context.Candles.Add(candle);
			_context.CandleIngredients.Add(candle.Ingredient);
			await _context.SaveChangesAsync();
			return candle;
		}

		public CandleItem? GetCandleById(int candleId) =>
			_context.Candles.Where(c => c.Id ==  candleId).FirstOrDefault();

		public CandleItem? GetCandleByName(string candleName) =>
			_context.Candles.Where(c => c.Name.Normalize() ==  candleName.Normalize()).FirstOrDefault();

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
