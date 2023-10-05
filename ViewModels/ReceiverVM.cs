namespace Road23.WebAPI.ViewModels
{
	public record class ReceiverVM
	{
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? FathersName { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string DeliveryAdress { get; set; } = null!;
    }
}
