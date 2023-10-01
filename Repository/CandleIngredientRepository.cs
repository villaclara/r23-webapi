using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class CandleIngredientRepository : ICandleIngredientRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public CandleIngredientRepository(ApplicationContext context)
		{
			_context = context;
		}


		public CandleIngredient? GetIngredientsByCandleId(int candleId) =>
			_context.CandleIngredients.Where(i => i.CandleId == candleId).FirstOrDefault();

		public async Task<bool> SetIngredientsByCandleId(int candleId, CandleIngredient ingredient)
		{
			var cingredient = GetIngredientsByCandleId(candleId);
			if (cingredient is not null)
			{
				await UpdateIngredientsByCandleIdAsync(candleId, ingredient);
				return true;
			}

			_context.CandleIngredients.Add(ingredient);
			return await SaveAsync();
		}

		public async Task<bool> UpdateIngredientsByCandleIdAsync(int candleId, CandleIngredient ingredient)
		{
			_context.CandleIngredients.Update(ingredient);

			return await SaveAsync();

		}
		public async Task<bool> DeleteIngredientsAsync(CandleIngredient ingredient)
		{
			_context.CandleIngredients.Remove(ingredient);
			return await SaveAsync();
		}

		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;
	}
}
