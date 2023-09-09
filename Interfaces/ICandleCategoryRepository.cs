using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleCategoryRepository
	{
		IList<CandleCategory> GetCategories();
		CandleCategory? GetCategoryById(int categoryId);
		Task<CandleCategory> CreateCategoryAsync(CandleCategory candleCategory);
		Task<bool> RemoveCategoryAsync(CandleCategory candleCategory);
		bool CategoryExistsById (string categoryName);

	}
}
