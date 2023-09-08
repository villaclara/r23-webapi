using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface ICandleCategoryRepository
	{
		IList<CandleCategory> GetCategories();
		CandleCategory? GetCategoryById(int categoryId);
		CandleCategory CreateCategory (CandleCategory candleCategory);
		CandleCategory RemoveCategory(CandleCategory candleCategory);

	}
}
