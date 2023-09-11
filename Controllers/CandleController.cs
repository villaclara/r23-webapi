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


		/// <summary>
		/// Gets all candles list in view, depending on parameter <paramref name="path"/>.
		/// </summary>
		/// <param name="path">"full" to get full info, "basic" is defaul</param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetCandles(string path = "basic")
		{
			var candles = _candleRepository.GetCandles();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// make full info to display
			if (path == "full")
			{
				IList<CandleItemFullVM> fullcandles = new List<CandleItemFullVM>();
				foreach (var candle in candles)
				{
					var ingredient = _ingredientRepository.GetIngredientsByCandleId(candle.Id);
					var category = _categoryRepository.GetCategoryById(candle.CategoryId);

					fullcandles.Add(candle.ConvertFromDefaultModel_ToFullVM(category, ingredient));
				}
				return Ok(fullcandles);

			}

			// make basic info to display
			ICollection<CandleItemBasicVM> basiccandles = new List<CandleItemBasicVM>();
			foreach (var candle in candles)
			{
				var category = _categoryRepository.GetCategoryById(candle.CategoryId);
				basiccandles.Add(candle.ConvertFromDefaultModel_ToBasicVM(category));
			}

			return Ok(basiccandles);
		}


		/// <summary>
		/// Receive Candle Info by <paramref name="candleId"/> and optional <paramref name="view"/>.
		/// </summary>
		/// <param name="candleId">Candle ID to find.</param>
		/// <param name="view">Optional parameter, might be 'basic' or 'full'.</param>
		/// <returns></returns>
		[HttpGet("cid={candleId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetCandleById(int candleId, string view = "basic")
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if (candle is null)
				return NotFound();

			var ingr = _ingredientRepository.GetIngredientsByCandleId(candleId);
			var ctgr = _categoryRepository.GetCategoryById(candle.CategoryId);

			if (view == "full")
			{
				var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
				return Ok(candleToDisplay1);
			}

			var candleToDisplay = candle.ConvertFromDefaultModel_ToBasicVM(ctgr);
			return Ok(candleToDisplay);

		}


		[HttpGet("cname={candleName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetCandleByName(string candleName, string view = "basic")
		{
			var candle = _candleRepository.GetCandleByName(candleName);
			if (candle is null)
				return NotFound();

			var ingr = _ingredientRepository.GetIngredientsByCandleId(candle.Id);
			var ctgr = _categoryRepository.GetCategoryById(candle.CategoryId);


			if (view == "full")
			{
				var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
				return Ok(candleToDisplay1);
			}

			var candleToDisplay = candle.ConvertFromDefaultModel_ToBasicVM(ctgr);
			return Ok(candleToDisplay);
		}



		// CandleItemFullVM has string Category field, int Id 
		// these fields is not needed when adding candle
		// so for now we just type wthatever in here, but possibly it needs to be re-written

		// also PHOTOLINK should be provided somehow
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(422)]
		public async Task<IActionResult> AddCandleAsync([FromQuery] int categoryId, [FromBody] CandleItemFullVM candleToAdd)
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

			var newCandle = candleToAdd.ConvertFromFullVM_ToDefaultModel(ctgr);
			await _candleRepository.CreateCandleAsync(newCandle);

			return Ok("Candle created");
		}



		[HttpDelete("cid={candleId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteCandleAsync(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if (candle is null)
				return NotFound();

			await _candleRepository.RemoveCandleAsync(candle);

			var ingredient = _ingredientRepository.GetIngredientsByCandleId(candleId);
			if (ingredient is not null)
				await _ingredientRepository.DeleteIngredientsAsync(ingredient);

			return Ok("Candle deleted.");
		}


		[HttpPut("cid={candleId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> UpdateCandleAsync(int candleId, [FromBody] CandleItemFullVM candleToUpdate)
		{
			if (candleToUpdate is null || candleToUpdate.Id != candleId)
				return BadRequest(ModelState);

			if (!_candleRepository.CandleExistsById(candleId))
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			var ctgr = _categoryRepository.GetCategoryByName(candleToUpdate.Category);
			if (ctgr is null)
				return NotFound();



			var ingrd = _ingredientRepository.GetIngredientsByCandleId(candleId);
			if (ingrd is null)
				return NotFound();
			ingrd.WickForDiameterCD = candleToUpdate.WickDiameterCM;
			ingrd.WaxNeededGram = candleToUpdate.WaxNeededGram;

			await _ingredientRepository.UpdateIngredientsByCandleIdAsync(candleId, ingrd);

			var cndl = new CandleItem
			{
				Name = candleToUpdate.Name,
				Id = candleToUpdate.Id,
				Description = candleToUpdate.Description,
				BurningTimeMins = candleToUpdate.BurningTimeMins,
				Category = ctgr,
				HeightCM = candleToUpdate.HeightCM,
				RealCost = candleToUpdate.RealCost,
				SellPrice = candleToUpdate.SellPrice,
			};
			await _candleRepository.UpdateCandleAsync(cndl);
			return Ok("Candle updated.");

		}


	}

}
