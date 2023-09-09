using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleItemRepository
	{
		IList<CandleItem> GetCandles();
		CandleItem? GetCandleById(int candleId);
		CandleItem? GetCandleByName(string candleName);
		Task<CandleItem> CreateCandleAsync (CandleItem candle);
		Task<CandleItem> UpdateCandle (CandleItem candle);
		Task<CandleItem> RemoveCandle (CandleItem candle);
		bool CandleExistsByName (string candleName);

	}
}
