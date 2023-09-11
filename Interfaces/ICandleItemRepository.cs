using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleItemRepository
	{
		IList<CandleItem> GetCandles();
		IEnumerable<CandleItem>? GetCandlesFromCategory(int categoryId);
		CandleItem? GetCandleById(int candleId);
		CandleItem? GetCandleByName(string candleName);
		Task<CandleItem> CreateCandleAsync (CandleItem candle);
		Task<CandleItem> UpdateCandleAsync(CandleItem candle);
		Task<CandleItem> RemoveCandleAsync(CandleItem candle);
		bool CandleExistsByName (string candleName);
		bool CandleExistsById (int candleId);

	}
}
