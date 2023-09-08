using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class CandleCategoryRepository : ICandleCategoryRepository
	{
		private readonly ApplicationContext _context;
		public CandleCategoryRepository(ApplicationContext context)
		{
			_context = context;
		}


		public CandleCategory CreateCategory(CandleCategory candleCategory)
		{
			_context.CandleCategories.Add(candleCategory);
			_context.SaveChanges();
			return candleCategory;
		}

		public IList<CandleCategory> GetCategories()
		{
			return _context.CandleCategories.ToList();
		}

		public CandleCategory? GetCategoryById(int categoryId)
		{
			throw new NotImplementedException();
		}

		public CandleCategory RemoveCategory(CandleCategory candleCategory)
		{
			throw new NotImplementedException();
		}
	}
}
