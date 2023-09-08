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

		[HttpGet("{categoryId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetCategoryById(int categoryId)
		{
			var category = _categoryRepository.GetCategoryById(categoryId);

			if (category is null)
				return NotFound();

			return Ok(category);
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

			return Ok("Category created");

		}


		[HttpDelete("{categoryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public IActionResult DeleteCategory (int categoryId)
		{
			var category = _categoryRepository.GetCategoryById(categoryId);
			if (category is null) 
				return NotFound();

			_categoryRepository.RemoveCategory(category);
			return Ok("Category deleted");
		}


	}



	public class CategoryViewModel
	{
		public string Name { get; set; }
	}
}
