using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
	public static class ExpenseExtensions
	{
		public static ExpenseVM ConvertFromModel_ToVM(this Expense model) =>
			new()
			{
				Id = model.Id,
				Cost = model.Cost,
				Date = DateOnly.FromDateTime(model.Date),
				Description = model.Description,
			};

		public static Expense ConvertFomVM_ToModel(this ExpenseVM expenseVM) =>
			new()
			{
				Id = expenseVM.Id,
				Cost = expenseVM.Cost,
				Date = expenseVM.Date.ToDateTime(TimeOnly.Parse("10:00PM")),
				Description = expenseVM.Description,
			};

	}
}
