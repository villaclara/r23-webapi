using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Utility.ExtensionMethods;
using Road23.WebAPI.ViewModels;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;

namespace Road23.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ExpenseController : ControllerBase
	{
		private readonly IExpenseRepository _expenseRepository;
		public ExpenseController(IExpenseRepository expenseRepository)
		{
			_expenseRepository = expenseRepository;
		}

		/// <summary>
		/// Gets the <see cref="Expense"/> and converts them to <see cref="ExpenseVM"/>. Then returns a list.
		/// </summary>
		/// <returns>List of <see cref="ExpenseVM"/> objects</returns>
		[HttpGet]
		public IActionResult GetExpenses()
		{
			var expenses = _expenseRepository.GetExpenses();

			IList<ExpenseVM> expensesVM = new List<ExpenseVM>();
			foreach(var expense in expenses)
			{
				expensesVM.Add(expense.ConvertFromModel_ToVM());
			}

			return Ok(expensesVM);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> CreateExpense([FromBody] ExpenseVM expenseToAdd)
		{
			// candle to Add is not set
			if (expenseToAdd is null)
				return BadRequest(ModelState);

			// incorrect model state
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// converting to model to add
			var expense = expenseToAdd.ConvertFomVM_ToModel();

			var isSuccess = await _expenseRepository.CreateExpenseAsync(expense);

			if (isSuccess == false)
			{
				ModelState.AddModelError("", "Internal error when creating expense.");
				return StatusCode(500, ModelState);
			}

			return Ok(expenseToAdd);

		}

		[HttpPut("eid={expenseId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] ExpenseVM expenseToUpdate)
		{
			if (expenseToUpdate is null || expenseToUpdate.Id != expenseId)
				return StatusCode(400, ModelState);

			if (!ModelState.IsValid)
				return StatusCode(400, ModelState);

			var expense = new Expense
			{
				Id = expenseId,
				Description = expenseToUpdate.Description,
				Date = expenseToUpdate.Date.ToDateTime(TimeOnly.Parse("10:00PM")),
				Cost = expenseToUpdate.Cost,
			};


			var isSuccess = await _expenseRepository.UpdateExpenseAsync(expense);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when updating expense.");
				return StatusCode(500, ModelState);
			}

			return Ok(expenseToUpdate);
		}


		[HttpDelete("eid={expenseId:int}")]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteExpense(int expenseId)
		{
			var expense = _expenseRepository.GetExpenses().Where(e => e.Id == expenseId).FirstOrDefault();
			if (expense is default(Expense))
				return NotFound();

			var isSuccess = await _expenseRepository.DeleteExpenseAsync(expense);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when deleting expense.");
				return StatusCode(500, ModelState);
			}

			return Ok($"Expense - {expenseId} deleted.");
		}


	}
}
