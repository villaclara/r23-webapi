using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using Road23.WebAPI.ViewModels;
using System.Reflection.Metadata.Ecma335;

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
		public async Task<IActionResult> CreateCategory([FromBody] CandleCategoryVM categoryToCreate)
		{
			if (categoryToCreate is null)
				return BadRequest(ModelState);

			if (_categoryRepository.CategoryExistsById(categoryToCreate.Name))
			{
				ModelState.AddModelError("", "Category already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ctgr = new CandleCategory { 
				Name = categoryToCreate.Name 
			};

			var result = await _categoryRepository.CreateCategoryAsync(ctgr);
			
			if (result is null)
			{
				ModelState.AddModelError("", "Something went wrong when creating category");
				return StatusCode(500, ModelState);
			}

			return Ok(result);
		}


		[HttpDelete("{categoryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteCategory(int categoryId)
		{
			var ctgr = _categoryRepository.GetCategoryById(categoryId);
			if (ctgr is null)
			{
				return NotFound();
			}

			return await _categoryRepository.RemoveCategoryAsync(ctgr) ? Ok("Category Deleted") : BadRequest(ModelState);
			
		}

	}

}
