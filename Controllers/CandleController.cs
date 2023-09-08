//using Microsoft.AspNetCore.Mvc;
//using Road23.WebAPI.Interfaces;
//using Road23.WebAPI.Models;
//using Road23.WebAPI.Repository;

//namespace Road23.WebAPI.Controllers
//{
//	[ApiController]
//	[Route("[api/controller]")]
//	public class CandleController : ControllerBase
//	{
//		private readonly ICandleItemRepository _candleRepository;
//		public CandleController(ICandleItemRepository candleRepository)
//		{
//			_candleRepository = candleRepository;
//		}

//		[HttpGet]
//		public IActionResult GetCandles()
//		{
//			var candles = _candleRepository.GetCandles();

//			if (!ModelState.IsValid)
//				return BadRequest(ModelState);

//			return Ok(candles);
//		}

//		//[HttpPost]
//		//public IActionResult CreateCandle([FromBody] Candle candleCreate)
//		//{
			
//		//}
//	}
//}
