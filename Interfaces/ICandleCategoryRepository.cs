using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleCategoryRepository
	{
		IList<CandleCategory> GetCategories();
		CandleCategory? GetCategoryById(int categoryId);
		CandleCategory? GetCategoryByName(string name);
		Task<CandleCategory> CreateCategoryAsync(CandleCategory candleCategory);
		Task<bool> RemoveCategoryAsync(CandleCategory candleCategory);
		Task<CandleCategory> UpdateCategoryAsync(CandleCategory candleCategory);
		bool CategoryExistsByName (string categoryName);
		bool CategoryExistsById (int categoryId);	
		bool CandlesExistInCategoryId (int categoryId);
	}
}
