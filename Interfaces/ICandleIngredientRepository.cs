using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleIngredientRepository
	{
		CandleIngredient? GetIngredientsByCandleId (int candleId);
		Task<bool> SetIngredientsByCandleId(int candleId, CandleIngredient ingredient);
		Task<bool> UpdateIngredientsByCandleId(int candleId, CandleIngredient ingredient);
	}
}
