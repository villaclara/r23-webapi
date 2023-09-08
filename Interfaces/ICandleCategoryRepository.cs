using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleCategoryRepository
	{
		IList<CandleCategory> GetCategories();
		CandleCategory? GetCategoryById(int categoryId);
		Task<CandleCategory> CreateCategory (CandleCategory candleCategory);
		Task<CandleCategory> RemoveCategory(CandleCategory candleCategory);

	}
}
