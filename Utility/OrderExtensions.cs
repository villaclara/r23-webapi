using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility
{
	public static class OrderExtensions
	{
		public static OrderFullVM ConvertFromDefaultOrder_ToFullVM(this Order order)
		{
			var orderFull = new OrderFullVM()
			{
				Id = order.Id,
				CustomerId = order.CustomerId,
				OrderDate = order.OrderDate,
				Promocode = order.Promocode,
				Comments = order.Comments,
				Receiver = new ReceiverVM
				{
					FullName = order.Receiver.FirstName + " " + order.Receiver.LastName + " " + order.Receiver.FathersName ?? "",
					PhoneNumber = order.Receiver.PhoneNumber,
					City = order.Receiver.City,
					DeliveryAdress = order.Receiver.DeliveryAdress,
				},
				TotalSum = order.TotalSum,
				OrderDetails = new List<OrderDetailsFullVM>()
			};

			foreach(var item in order.OrderDetails)
			{
				orderFull.OrderDetails.Add(new OrderDetailsFullVM()
				{
					CandleId = item.CandleId,
					CandleQuantity = item.CandleQuantity,
				});
			}

			return orderFull;
		}

	}
}
