using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;
using Road23.WebAPI.Utility.ExtensionMethods;
using Microsoft.AspNetCore.WebUtilities;
using System.IO;
using System.Reflection.Metadata;
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
		/// Gets all candles list in view, depending on parameter <paramref name="view"/>.
		/// </summary>
		/// <param name="view">"full" to get full info, "basic" is defaul</param>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetCandles(string view = "basic")
		{
			var candles = _candleRepository.GetCandles();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// make full info to display
			if (view == "full")
			{
				IList<CandleItemFullVM> fullcandles = new List<CandleItemFullVM>();
				foreach (var candle in candles)
				{
					fullcandles.Add(candle.ConvertFromDefaultModel_ToFullVM());
				}
				return Ok(fullcandles);
			}

			// make basic info to display
			ICollection<CandleItemBasicVM> basiccandles = new List<CandleItemBasicVM>();
			foreach (var candle in candles)
			{
				basiccandles.Add(candle.ConvertFromDefaultModel_ToBasicVM());
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

			if (view == "full")
			{
				var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM();
				return Ok(candleToDisplay1);
			}

			var candleToDisplay = candle.ConvertFromDefaultModel_ToBasicVM();
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

			if (view == "full")
			{
				var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM();
				return Ok(candleToDisplay1);
			}

			var candleToDisplay = candle.ConvertFromDefaultModel_ToBasicVM();
			return Ok(candleToDisplay);
		}


		[HttpGet("catid={categoryId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetCandlesByCategoryId(int categoryId, string view = "basic")
		{
			var candles = _candleRepository.GetCandlesFromCategory(categoryId);
			if (candles is default(IEnumerable<CandleItem>) || !candles.Any())
				return NotFound($"No candles for category id {categoryId}");

			if (view == "full")
			{
				IList<CandleItemFullVM> fullcandles = new List<CandleItemFullVM>();
				foreach (var candle in candles)
				{
					fullcandles.Add(candle.ConvertFromDefaultModel_ToFullVM());
				}
				return Ok(fullcandles);
			}

			// make basic info to display
			ICollection<CandleItemBasicVM> basiccandles = new List<CandleItemBasicVM>();
			foreach (var candle in candles)
			{
				basiccandles.Add(candle.ConvertFromDefaultModel_ToBasicVM());
			}

			return Ok(basiccandles);
		}

		// Get the image as base64 string for image by candle id
		[HttpGet("imgForId={candleId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetImageByCandleId(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if(candle is null)
			{
				return NotFound();
			}

			if(candle.PhotoLink is null)
			{
				return Ok("");
			}

			var imgBytes = System.IO.File.ReadAllBytes(candle.PhotoLink);

			string str = Convert.ToBase64String(imgBytes);
			
			return Ok(str);

		}


		// CandleItemFullVM has int Id 
		// these fields is not needed when adding candle
		// so for now we just type wthatever in here, but possibly it needs to be re-written

		// also PHOTOLINK should be provided somehow
		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(422)]
		public async Task<IActionResult> AddCandleAsync([FromBody] CandleItemFullVM candleToAdd)
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

			// category does not exists by received name
			// we add new category then
			var ctgr = _categoryRepository.GetCategoryByName(candleToAdd.Category);
			if (ctgr is null)
			{
				ctgr = new CandleCategory { Name = candleToAdd.Category };
				await _categoryRepository.CreateCategoryAsync(ctgr);
			}

			var newCandle = candleToAdd.ConvertFromFullVM_ToDefaultModel(ctgr);
			var isSuccess = await _candleRepository.CreateCandleAsync(newCandle);

			if (isSuccess == false)
			{
				ModelState.AddModelError("", "Internal error when creating candle.");
				return StatusCode(500, ModelState);
			}


			return Ok(candleToAdd);
		}


		[HttpPost("upload")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> AddImageToCandleId([FromQuery] int candleId, [FromForm] IFormFile file)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if (candle is null)
			{
				return StatusCode(404);
			}

			if(file != null)
			{

				// create directory if not exists
				if(!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), ConstantsClass.CANDLE_IMAGES_DIRECTORYNAME)))
				{
					Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), ConstantsClass.CANDLE_IMAGES_DIRECTORYNAME));
				}

				// unique filename based on current time
				string filename = DateTime.Now.ToString("HHmmssddMMyyyy") + $"{file.FileName}";
				var pathToPhoto = Path.Combine(Directory.GetCurrentDirectory(), ConstantsClass.CANDLE_IMAGES_DIRECTORYNAME, filename);
				
				using FileStream fs = new(pathToPhoto, FileMode.Create);
				await file.CopyToAsync(fs);
			
				
				candle.PhotoLink = pathToPhoto;
				await _candleRepository.UpdateCandleAsync(candle);

				return Ok();
			}


			return StatusCode(500);
		}


		[HttpDelete("cid={candleId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteCandleAsync(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);
			if (candle is null)
				return NotFound();

			var isSuccess = await _candleRepository.RemoveCandleAsync(candle);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when creating candle.");
				return StatusCode(500, ModelState);
			}
			
			var ingredient = _ingredientRepository.GetIngredientsByCandleId(candleId);
			if (ingredient is not null)
				await _ingredientRepository.DeleteIngredientsAsync(ingredient);

			return Ok($"Candle - {candleId} deleted.");
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
			{
				ctgr = new CandleCategory { Name = candleToUpdate.Category };
				await _categoryRepository.CreateCategoryAsync(ctgr);
			}

			var ingrd = _ingredientRepository.GetIngredientsByCandleId(candleId);
			if (ingrd is null)
				return NotFound();
			await _ingredientRepository.DeleteIngredientsAsync(ingrd);

			var cnd = _candleRepository.GetCandleById(candleId)!;
			cnd.Name = candleToUpdate.Name;
			cnd.Description = candleToUpdate.Description;
			cnd.Category = ctgr;
			cnd.RealCost = candleToUpdate.RealCost;
			cnd.SellPrice = candleToUpdate.SellPrice;
			cnd.HeightCM = candleToUpdate.HeightCM;
			cnd.BurningTimeMins = candleToUpdate.BurningTimeMins;
			cnd.Ingredient = new CandleIngredient
			{
				WickForDiameterCD = candleToUpdate.WickDiameterCM,
				WaxNeededGram = candleToUpdate.WaxNeededGram,
				CandleId = cnd.Id
			};

			var isSuccess = await _candleRepository.UpdateCandleAsync(cnd);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when creating candle.");
				return StatusCode(500, ModelState);
			}
			return Ok(candleToUpdate);

		}


	}

}
