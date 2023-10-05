using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface IReceiverRepository
	{
		bool ExistsByPhoneNumber(string phoneNumber);
		ICollection<Receiver> GetReceivers();
		ICollection<Receiver> GetReceiverByPhone(string phoneNumber);
		Task<bool> AddReceiverAsync(Receiver receiver);
		Task<bool> EditReceiverAsync(Receiver receiver);
		Task<bool> DeleteReceiverAsync(Receiver receiver);
		
	}
}
