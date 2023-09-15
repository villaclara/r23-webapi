using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;
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



		[HttpPost]
		public async Task<IActionResult> AddOrderAsync([FromBody] OrderFullVM orderToAdd)
		{
			// mapping from OrderFullVM Viewmodel to Order model
			var ordr = new Order
			{
				OrderDate = orderToAdd.OrderDate,
				Promocode = orderToAdd.Promocode,
				TotalSum = orderToAdd.TotalSum,
				CustomerId = orderToAdd.CustomerId,
				Comments = orderToAdd.Comments,
				Receiver = orderToAdd.Receiver,
			};
			List<OrderDetails> orderDetails = new();
			foreach (var item in orderToAdd.OrderDetails)
			{
				orderDetails.Add(new OrderDetails
				{
					CandleId = item.CandleId,
					CandleQuantity = item.CandleQuantity,
				});
			}
			ordr.OrderDetails = orderDetails;
			
			// adding order - it will add both Order and OrderDetails to db
			await _orderRepository.CreateOrderAsync(ordr);
			return Ok(ordr);

		}

		[HttpDelete("oid={orderId:int}")]
		[ProducesResponseType(404)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteOrderAsync(int orderId)
		{
			var order = _orderRepository.GetOrderById(orderId);
			if (order is null)
				return NotFound($"Order {orderId} - Not found.");

			var deleted = await _orderRepository.DeleteOrderAsync(order);

			var details = _detailsRepository.GetOrderDetailsByOrderId(orderId);
			foreach (var d in details)
			{
				await _detailsRepository.RemoveOrderDetailsFromOrderAsync(orderId, d);
			}

			return Ok($"Order deleted - {deleted.Id}");
		}



		// TO DO 
		[HttpPut("oid={orderId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> EditOrderAsync(int orderId, [FromBody] OrderFullVM orderToUpdate)
		{
			if (orderToUpdate is null || orderToUpdate.Id != orderId)
				return StatusCode(400, ModelState);

			if (!_orderRepository.OrderExistsById(orderId))
				return StatusCode(404, $"Order {orderId} - Not found.");

			if (!ModelState.IsValid)
				return StatusCode(400, ModelState);


			// deleting all OrderDetails related to order by id
			// because otherwise It adds new details instead of changing current one
			await _detailsRepository.RemoveAllOrderDetailsByOrderId(orderId);

			// new order from OrderVM
			var ordr = new Order
			{
				Id = orderId,
				OrderDate = orderToUpdate.OrderDate,
				Promocode = orderToUpdate.Promocode,
				TotalSum = orderToUpdate.TotalSum,
				CustomerId = orderToUpdate.CustomerId,
				Comments = orderToUpdate.Comments,
				Receiver = orderToUpdate.Receiver,
				OrderDetails = new List<OrderDetails>()
			};
			foreach (var item in orderToUpdate.OrderDetails)
			{
				ordr.OrderDetails.Add(new OrderDetails
				{
					OrderId = orderId,
					CandleId = item.CandleId,
					CandleQuantity = item.CandleQuantity,
				});
			}

			// Updating Order also adds all OrderDetails to DB
			await _orderRepository.UpdateOrderAsync(ordr);
			return Ok("Order updated.");
		}

		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrders()
		{
			// getting orders
			var orders = _orderRepository.GetOrders();
			if (!orders.Any())
				return StatusCode(404, "Orders Not Found");

			// getting OrderDetails for each order and 
			// mapping from Order model to OrderFullVM viewmodel
			IList<OrderFullVM> orderVMs = new List<OrderFullVM>();
			foreach(var o in orders)
			{
				o.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(o.Id);
				orderVMs.Add(o.ConvertFromDefaultOrder_ToFullVM());
			}

			if (!ModelState.IsValid)
				return StatusCode(400, ModelState);
			
			return Ok(orderVMs);
		}

		[HttpGet("oid={orderId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrderById(int orderId)
		{
			var order = _orderRepository.GetOrderById(orderId);
			if (order is null)
				return NotFound($"Order {orderId} - Not found.");

			order.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(orderId);
			var orderVM = order.ConvertFromDefaultOrder_ToFullVM();
			
			return Ok(orderVM);

		}

		[HttpGet("cid={customerId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByCustomerId(int customerId)
		{
			var orders = _orderRepository.GetOrdersByCustomerId(customerId);
			if (!orders.Any())
				return StatusCode(404, $"Orders by Customer {customerId} - Not found.");


			// getting OrderDetails for each order and 
			// mapping from Order model to OrderFullVM viewmodel
			IList<OrderFullVM> orderVMs = new List<OrderFullVM>();
			foreach (var o in orders)
			{
				o.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(o.Id);
				orderVMs.Add(o.ConvertFromDefaultOrder_ToFullVM());
			}

			return Ok(orderVMs);

		}

		[HttpGet("date={date}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByDate(DateOnly date)
		{
			var orders = _orderRepository.GetOrdersByDate(date);
			if (!orders.Any())
				return StatusCode(404, ModelState);

			// getting OrderDetails for each order and 
			// mapping from Order model to OrderFullVM viewmodel
			IList<OrderFullVM> orderVMs = new List<OrderFullVM>();
			foreach (var o in orders)
			{
				o.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(o.Id);
				orderVMs.Add(o.ConvertFromDefaultOrder_ToFullVM());
			}

			return Ok(orderVMs);

		}

		[HttpGet("minsum={minSum:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByMinSum(int minSum)
		{
			var orders = _orderRepository.GetOrdersByMinimalSum(minSum);
			if (!orders.Any())
				return StatusCode(404);

			// getting OrderDetails for each order and 
			// mapping from Order model to OrderFullVM viewmodel
			IList<OrderFullVM> orderVMs = new List<OrderFullVM>();
			foreach (var o in orders)
			{
				o.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(o.Id);
				orderVMs.Add(o.ConvertFromDefaultOrder_ToFullVM());
			}

			return Ok(orderVMs);
		}

		[HttpGet("maxsum={maxSum:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public IActionResult GetOrdersByMaxSum(int maxSum)
		{
			var orders = _orderRepository.GetOrdersByMaximalSum(maxSum);
			if (!orders.Any())
				return NotFound();

			// getting OrderDetails for each order and 
			// mapping from Order model to OrderFullVM viewmodel
			IList<OrderFullVM> orderVMs = new List<OrderFullVM>();
			foreach (var o in orders)
			{
				o.OrderDetails = _detailsRepository.GetOrderDetailsByOrderId(o.Id);
				orderVMs.Add(o.ConvertFromDefaultOrder_ToFullVM());
			}

			return Ok(orderVMs);
		}

	}
}
