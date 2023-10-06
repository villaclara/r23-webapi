using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
    public static class ReceiverExtensions
    {
        public static Receiver ConvertFromReceiverVM_ToReceiverModel(this ReceiverVM receiverVM)
        {
            var names = receiverVM.FullName.TrimEnd().Split(' ');
            string n1, n2, n3;
            if (names.Length == 3)
            {
                n1 = names[0];
                n2 = names[1];
                n3 = names[2];
            }
            else if (names.Length == 2)
            {
                n1 = names[0];
                n2 = names[1];
                n3 = "";
            }
            else
            {
                n1 = names[0];
                n2 = "";
                n3 = "";
            }
            Receiver receiver = new()
            {
                FirstName = n1,
                LastName = n2,
                FathersName = n3,
                City = receiverVM.City,
                DeliveryAdress = receiverVM.DeliveryAdress,
                PhoneNumber = receiverVM.PhoneNumber,
            };

            return receiver;
        }
    }
}
