using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderDetailsRepository _detailsRepository;
		public OrderController(IOrderRepository orderRepository, IOrderDetailsRepository detailsRepository)
		{
			_orderRepository = orderRepository;
			_detailsRepository = detailsRepository;
		}



		// TO DO
		[HttpPost]
		public async Task<IActionResult> AddOrderAsync([FromBody] OrderToAddVM orderToAdd)
		{

			var ordr = new Order
			{
				OrderDate = orderToAdd.OrderDate,
				Promocode = orderToAdd.Promocode,
				TotalSum = orderToAdd.TotalSum,
				CustomerId = orderToAdd.CustomerId,
			};

			//await _orderRepository.CreateOrderAsync(ordr);

			List<OrderDetails> orderDetails = new List<OrderDetails>();

			foreach(var item in orderToAdd.OrderDetails)
			{
				orderDetails.Add(new OrderDetails
				{
					CandleId = item.CandleId,
					CandleQuantity = item.CandleQuantity,
				});

				//await _detailsRepository.AddOrderDetailsToOrderAsync()
			}

			ordr.OrderDetails = orderDetails;
			await _orderRepository.CreateOrderAsync(ordr);
			return Ok(ordr);

		}



		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrders()
		{
			var orders = _orderRepository.GetOrders();
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			
			return Ok(orders);
		}

		[HttpGet("oid={orderId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrderById(int orderId, string view = "basic")
		{
			var order = _orderRepository.GetOrderById(orderId);
			if (order is null)
				return NotFound();

			//if (view == "full")
			//{
			//	var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
			//	return Ok(candleToDisplay1);
			//}

			
			return Ok(order);

		}

		[HttpGet("cid={customerId}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByCustomerId(int customerId, string view = "basic")
		{
			var orders = _orderRepository.GetOrdersByCustomerId(customerId);
			if (orders is null)
				return NotFound();

			//if (view == "full")
			//{
			//	var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
			//	return Ok(candleToDisplay1);
			//}


			return Ok(orders);

		}

		[HttpGet("date={date}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByDate(DateOnly date, string view = "basic")
		{
			var orders = _orderRepository.GetOrdersByDate(date);
			if (orders is null)
				return NotFound();

			//if (view == "full")
			//{
			//	var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
			//	return Ok(candleToDisplay1);
			//}


			return Ok(orders);

		}

		[HttpGet("sum={minSum}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByMinSum(int minSum, string view = "basic")
		{
			var orders = _orderRepository.GetOrdersByMinimalSum(minSum);
			if (orders is null)
				return NotFound();

			//if (view == "full")
			//{
			//	var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
			//	return Ok(candleToDisplay1);
			//}


			return Ok(orders);

		}

		[HttpGet("sum={maxSum}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByMaxSum(int maxSum, string view = "basic")
		{
			var orders = _orderRepository.GetOrdersByMinimalSum(maxSum);
			if (orders is null)
				return NotFound();

			//if (view == "full")
			//{
			//	var candleToDisplay1 = candle.ConvertFromDefaultModel_ToFullVM(ctgr, ingr);
			//	return Ok(candleToDisplay1);
			//}


			return Ok(orders);

		}

	}
}
