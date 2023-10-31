using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class ExpenseRepository : IExpenseRepository, IContextSave
	{
		private readonly ApplicationContext _context;

		public ExpenseRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<bool> CreateExpenseAsync(Expense expense)
		{
			_context.Expenses.Add(expense);
			return await SaveAsync();
		}

		public async Task<bool> DeleteExpenseAsync(Expense expense)
		{
			_context.Expenses.Remove(expense);
			return await SaveAsync();
		}

		public IList<Expense> GetExpenses() =>
			_context.Expenses.ToList();


		public async Task<bool> UpdateExpenseAsync(Expense expense)
		{
			_context.Expenses.Update(expense);
			return await SaveAsync();
		}

		public async Task<bool> SaveAsync() => 
			await _context.SaveChangesAsync() > 0;
	}
}
