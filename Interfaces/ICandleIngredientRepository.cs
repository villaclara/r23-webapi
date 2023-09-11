using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleIngredientRepository
	{
		CandleIngredient? GetIngredientsByCandleId (int candleId);
		Task<bool> SetIngredientsByCandleId(int candleId, CandleIngredient ingredient);
		Task<bool> UpdateIngredientsByCandleIdAsync(int candleId, CandleIngredient ingredient);
		Task<bool> DeleteIngredientsAsync(CandleIngredient ingredient);
	}
}
