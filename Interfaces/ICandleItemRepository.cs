using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleItemRepository
	{
		IList<CandleItem> GetCandles();
		IEnumerable<CandleItem>? GetCandlesFromCategory(int categoryId);
		CandleItem? GetCandleById(int candleId);
		CandleItem? GetCandleByName(string candleName);
		Task<bool> CreateCandleAsync (CandleItem candle);
		Task<bool> UpdateCandleAsync(CandleItem candle);
		Task<bool> RemoveCandleAsync(CandleItem candle);
		bool CandleExistsByName (string candleName);
		bool CandleExistsById (int candleId);

	}
}
