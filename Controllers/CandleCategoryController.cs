using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using Road23.WebAPI.Utility.ExtensionMethods;
using Road23.WebAPI.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace Road23.WebAPI.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class CandleCategoryController : ControllerBase
	{
		private readonly ICandleCategoryRepository _categoryRepository;
		private readonly ICandleItemRepository _candleRepository;
		public CandleCategoryController(ICandleCategoryRepository categoryRepository, ICandleItemRepository candleItemRepository)
		{
			_categoryRepository = categoryRepository;
			_candleRepository = candleItemRepository;
		}

		[HttpGet]
		public IActionResult GetCategories()
		{
			var categories = _categoryRepository.GetCategories();

			IList<CandleCategoryFullVM> ctgrs = new List<CandleCategoryFullVM>();
			foreach(var category in categories)
			{
				ctgrs.Add(category.ConvertFromDefaulModel_ToFullVM());
			}

			return Ok(ctgrs);
		}

		[HttpGet("{categoryId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetCategoryById(int categoryId)
		{
			var category = _categoryRepository.GetCategoryById(categoryId);
			if (category is default(CandleCategory))
				//return NotFound($"Category with id {categoryId} not found.");
				return new NotFoundResult();

			var ctgr = category.ConvertFromDefaulModel_ToFullVM();

			return Ok(ctgr);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategoryAsync([FromQuery] string categoryToCreate)
		{
			if (categoryToCreate is null)
				return BadRequest(ModelState);

			if (_categoryRepository.CategoryExistsByName(categoryToCreate))
			{
				ModelState.AddModelError("", "Category already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ctgr = new CandleCategory { 
				Name = categoryToCreate
			};

			var result = await _categoryRepository.CreateCategoryAsync(ctgr);
			
			if (result is false)
			{
				ModelState.AddModelError("", "Something went wrong when creating category");
				return StatusCode(500, ModelState);
			}

			return Ok(categoryToCreate);
		}


		[HttpDelete("{categoryId}")]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
		{
			var ctgr = _categoryRepository.GetCategoryById(categoryId);
			if (ctgr is null)
			{
				return NotFound();
			}

			if (_categoryRepository.CandlesExistInCategoryId(ctgr.Id))
			{
				return StatusCode(400, "The Candles exists in this category. Delete Candles first.");
			}
			return await _categoryRepository.RemoveCategoryAsync(ctgr) ? Ok("Category Deleted") : StatusCode(500, ModelState);
			
		}


		[HttpPut("{categoryId}")]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> UpdateCategoryAsync(int categoryId, [FromBody] string newCategoryName)
		{
			if (!_categoryRepository.CategoryExistsById(categoryId))
				return NotFound();
			
			var category = new CandleCategory
			{
				Id = categoryId,
				Name = newCategoryName
			};

			return await _categoryRepository.UpdateCategoryAsync(category) ? Ok("Category updated.") : StatusCode(500, ModelState);

		}
	}

}
