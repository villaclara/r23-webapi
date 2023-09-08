using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleItemRepository
	{
		IList<CandleItem> GetCandles();
		CandleItem? GetCandleById(int candleId);
		CandleItem? GetCandleByName(string candleName);
		CandleItem CreateCandle (CandleItem candle);
		CandleItem UpdateCandle (CandleItem candle);
		CandleItem RemoveCandle (CandleItem candle);
		bool CandleExists (CandleItem candle);

	}
}
