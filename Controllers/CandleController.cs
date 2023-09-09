using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using Road23.WebAPI.ViewModels;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CandleController : ControllerBase
	{
		private readonly ICandleItemRepository _candleRepository;
		private readonly ICandleCategoryRepository _categoryRepository;
		private readonly ICandleIngredientRepository _ingredientRepository;
		public CandleController(ICandleItemRepository candleRepository, 
								ICandleCategoryRepository categoryRepository, 
								ICandleIngredientRepository ingredientRepository)
		{
			_candleRepository = candleRepository;
			_categoryRepository = categoryRepository;
			_ingredientRepository = ingredientRepository;
		}

		[HttpGet]
		public IActionResult GetCandles()
		{
			var candles = _candleRepository.GetCandles();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			ICollection<CandleItemBasicVM> cndls = new List<CandleItemBasicVM>();
			foreach(var candle in candles)
			{
				var ctgr = _categoryRepository.GetCategoryById(candle.CategoryId);
				cndls.Add(candle.MakeBasicCandleToDisplay(ctgr));
			}

			return Ok(cndls);
		}

		[HttpGet("{candleId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetCandleById(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			var ingr = _ingredientRepository.GetIngredientsByCandleId(candleId);

			if (candle is null || ingr is null)
				return NotFound();

			var ctgr = _categoryRepository.GetCategoryById(candle.CategoryId)?.Name;
			if (ctgr is null)
				return NotFound();

			var candleModel = new CandleItemToDisplayVM
			{
				Name = candle.Name,
				Description = candle.Description,
				Category = ctgr,
				PhotoLink = candle.PhotoLink,
				RealCost = candle.RealCost,
				SellPrice = candle.SellPrice,
				BurningTimeMins = candle.BurningTimeMins,
				WaxNeededGram = ingr.WaxNeededGram,
				WickDiameterCM = ingr.WickForDiameterCD
			};

			return Ok(candleModel);
		}

		[HttpGet("{candleName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public IActionResult GetCandleByName(string candleName)
		{
			var candle = _candleRepository.GetCandleByName(candleName);
			if (candle is null)
				return NotFound();

			var ingredient = _ingredientRepository.GetIngredientsByCandleId(candle.Id);
			var category = _categoryRepository.GetCategoryById(candle.CategoryId);


			var candleModel = candle.MakeCandleToDisplay(category, ingredient);

			return Ok(candleModel);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> AddCandle([FromQuery] int categoryId, [FromBody] CandleItemVM candleToAdd)
		{
			// candle to Add is not set
			if (candleToAdd is null)
				return BadRequest(ModelState);

			// candle already exists
			if (_candleRepository.CandleExistsByName(candleToAdd.Name))
			{
				ModelState.AddModelError("", "Candle already exists");
				return StatusCode(422, ModelState);
			}

			// incorrect model state
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			
			// category does not exists by given ID in query
			var ctgr = _categoryRepository.GetCategoryById(categoryId);
			if (ctgr is null)
			{
				ModelState.AddModelError("", "Categore does not exist");
				return StatusCode(400, ModelState);
			}


			var cat = new CandleItem
			{
				Name = candleToAdd.Name,
				Description = candleToAdd.Description,
				PhotoLink = candleToAdd.PhotoLink,
				RealCost = candleToAdd.RealCost,
				SellPrice = candleToAdd.SellPrice,
				BurningTimeMins = candleToAdd.BurningTimeMins,
				Category = ctgr,
				Ingredient = new CandleIngredient
				{
					WaxNeededGram = candleToAdd.WaxNeededGram,
					WickForDiameterCD = candleToAdd.WickDiameterCM
				}
			};

			await _candleRepository.CreateCandleAsync(cat);

			return Ok("Candle created");
		}



		[HttpDelete("{candleId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public IActionResult DeleteCategory(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if (candle is null)
				return NotFound();

			_candleRepository.RemoveCandle(candle);
			return Ok("Category deleted");
		}

	}

}
