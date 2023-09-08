using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;

namespace Road23.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CandleCategoryController : ControllerBase
	{
		private readonly ICandleCategoryRepository _categoryRepository;
		public CandleCategoryController(ICandleCategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		[HttpGet]
		public IActionResult GetCategories()
		{
			var categories = _categoryRepository.GetCategories();

			return Ok(categories);
		}

		[HttpPost]
		public IActionResult CreateCategory([FromBody] CategoryViewModel categoryToCreate)
		{
			if (categoryToCreate is null)
				return BadRequest(ModelState);

			var existingCategory = _categoryRepository.GetCategories().Where(c => c.Name.Trim().ToLower() == categoryToCreate.Name.Trim().ToLower()).FirstOrDefault();

			if (existingCategory is not null)
			{
				ModelState.AddModelError("", "Category already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var cat = new CandleCategory { Name =  categoryToCreate.Name };

			_categoryRepository.CreateCategory(cat);

			return Ok("Created");

		}
	}



	public class CategoryViewModel
	{
		public string Name { get; set; }
	}
}
