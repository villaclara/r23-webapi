using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IExpenseRepository
	{
		IList<Expense> GetExpenses();
		Task<bool> CreateExpenseAsync(Expense expense);
		Task<bool> UpdateExpenseAsync(Expense expense);
		Task<bool> DeleteExpenseAsync(Expense expense);
	}
}
