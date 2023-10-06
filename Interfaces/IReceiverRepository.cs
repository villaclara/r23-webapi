using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IReceiverRepository
	{
		bool ExistsByPhoneNumber(string phoneNumber);
		ICollection<Receiver> GetReceivers();
		Receiver GetReceiverByPhone(string phoneNumber);
		Receiver GetReceiverByOrderId(int orderId);
		Task<bool> AddReceiverAsync(Receiver receiver);
		Task<bool> EditReceiverAsync(Receiver receiver);
		Task<bool> DeleteReceiverAsync(Receiver receiver);
		
	}
}
