namespace Road23.WebAPI.ViewModels
{
	public record class ReceiverVM
	{
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string DeliveryAdress { get; set; } = null!;
    }
}
