using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleIngredientRepository
	{
		CandleIngredient? GetIngredientsByCandleId (int candleId);
		Task<bool> SetIngredientsById(int candleId, CandleIngredient ingredient);
		Task<bool> UpdateIngredientsById(int candleId, CandleIngredient ingredient);
	}
}
