using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;

namespace Road23.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CandleController : ControllerBase
	{
		private readonly ICandleItemRepository _candleRepository;
		public CandleController(ICandleItemRepository candleRepository)
		{
			_candleRepository = candleRepository;
		}

		[HttpGet]
		public IActionResult GetCandles()
		{
			var candles = _candleRepository.GetCandles();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			return Ok(candles);
		}

		[HttpGet("{candleId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetCandleById(int candleId)
		{
			var candle = _candleRepository.GetCandleById(candleId);

			if (candle is null)
				return NotFound();

			return Ok(candle);
		}

		[HttpGet("{candleName}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetCandleByName(string candleName)
		{
			var candle = _candleRepository.GetCandleByName(candleName);

			if (candle is null)
				return NotFound();

			return Ok(candle);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult AddCandle([FromBody] CandleVieModel candleToAdd)
		{
			if (candleToAdd is null)
				return BadRequest(ModelState);

			var existingCandle = _candleRepository.GetCandles().Where(c => c.Name.Normalize() == candleToAdd.Name.Normalize()).FirstOrDefault();

			if (existingCandle is not null)
			{
				ModelState.AddModelError("", "Candle already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var cat = new CandleItem
			{
				Name = candleToAdd.Name,
				Description = candleToAdd.Description,
				PhotoLink = candleToAdd.PhotoLink,
				RealCost = candleToAdd.RealCost,
				SellPrice = candleToAdd.SellPrice,
				BurningTimeMins = candleToAdd.BurningTimeMins,
				Category = candleToAdd.Category,
				Ingredient = candleToAdd.Ingredient,
			};

			_candleRepository.CreateCandle(cat);

			return Ok("Category created");
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

	public class CandleVieModel
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? PhotoLink { get; set; }
		public decimal RealCost { get; set; }
		public decimal SellPrice { get; set; }
		public int? BurningTimeMins { get; set; }
		public CandleCategory Category { get; set; } = null!;
		public CandleIngredient Ingredient { get; set; } = null!;
	}
}
