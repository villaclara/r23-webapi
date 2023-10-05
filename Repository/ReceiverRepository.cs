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

		public Task<bool> AddReceiverAsync(Receiver receiver)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteReceiverAsync(Receiver receiver)
		{
			throw new NotImplementedException();
		}

		public Task<bool> EditReceiverAsync(Receiver receiver)
		{
			throw new NotImplementedException();
		}

		public bool ExistsByPhoneNumber(string phoneNumber) =>
			_context.Receivers.Where(r => r.PhoneNumber == phoneNumber).Any();

		public ICollection<Receiver> GetReceiverByPhone(string phoneNumber)
		{
			throw new NotImplementedException();
		}

		public ICollection<Receiver> GetReceivers()
		{
			throw new NotImplementedException();
		}

		public Task<bool> SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
