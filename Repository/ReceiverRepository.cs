using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Repository
{
	public class ReceiverRepository : IReceiverRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public ReceiverRepository(ApplicationContext context)
		{
			_context = context;
		}	

		public async Task<bool> AddReceiverAsync(Receiver receiver)
		{
			_context.Receivers.Add(receiver);
			return await SaveAsync();
		}

		public async Task<bool> DeleteReceiverAsync(Receiver receiver)
		{
			_context.Receivers.Remove(receiver);
			return await SaveAsync();
		}

		public async Task<bool> EditReceiverAsync(Receiver receiver)
		{
			_context.Receivers.Update(receiver);
			return await SaveAsync();
		}

		public bool ExistsByPhoneNumber(string phoneNumber) =>
			_context.Receivers.Where(r => r.PhoneNumber == phoneNumber).Any();

		public Receiver GetReceiverByOrderId(int orderId) =>
			throw new NotImplementedException();

		public Receiver GetReceiverByPhone(string phoneNumber) =>
			_context.Receivers.Where(r => r.PhoneNumber.Trim() == phoneNumber.Trim()).FirstOrDefault()!;

		public ICollection<Receiver> GetReceivers() =>
			_context.Receivers.ToList();

		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;
	}
}
