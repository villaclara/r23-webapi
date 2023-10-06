using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility.ModelsValidation;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
    public static class OrderExtensions
    {

        public static bool Validate(this OrderFullVM orderFullVM)
        {
            bool phoneValid = new ReceiverValidationService().ValidatePhoneNumber(orderFullVM.Receiver.PhoneNumber, isRequired: true);
            if (!phoneValid)
            {
                return false;
            }

            bool sumValid = new OrderValidationService().ValidateSum(orderFullVM.TotalSum);
            if (!sumValid)
            {
                return false;
            }

            return true;
        }

        public static OrderFullVM ConvertFromDefaultOrder_ToFullVM(this Order order)
        {
            string fathersname = order.Receiver.FathersName?.Length > 0 ? " " + order.Receiver.FathersName : "";
            
            var orderFull = new OrderFullVM()
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Promocode = order.Promocode,
                Comments = order.Comments,
                Receiver = new ReceiverVM
                {
                    FullName = order.Receiver.FirstName + " " + order.Receiver.LastName + fathersname,
                    PhoneNumber = order.Receiver.PhoneNumber.Trim(),
                    City = order.Receiver.City.Trim(),
                    DeliveryAdress = order.Receiver.DeliveryAdress.Trim(),
                },
                ReceiverRepeat = order.ReceiverRepeat,
                TotalSum = order.TotalSum,
                OrderDetails = new List<OrderDetailsFullVM>()
            };

            foreach (var item in order.OrderDetails)
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
