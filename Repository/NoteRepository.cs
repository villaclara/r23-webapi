using Road23.WebAPI.Database;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Utility;

namespace Road23.WebAPI.Repository
{
	public class NoteRepository : INoteRepository, IContextSave
	{
		private readonly ApplicationContext _context;
		public NoteRepository(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<bool> AddNoteAsync(Note note)
		{
			_context.Notes.Add(note);
			return await SaveAsync();
		}

		public async Task<bool> DeleteNoteAsync(Note note)
		{
			_context.Notes.Remove(note);
			return await SaveAsync();
		}

		public ICollection<Note> GetAllNotes() =>
			_context.Notes.ToList();

		public ICollection<Note> GetAllNotesFromDate(DateOnly date) =>
			_context.Notes.Where(n => n.NoteDate == date.ToDateTime(TimeOnly.Parse(ConstantsClass.DEFAULT_TIMEONLY_VALUE))).ToList();

		public async Task<bool> UpdateNoteAsync(Note note)
		{
			_context.Notes.Update(note);
			return await SaveAsync();
		}

		public async Task<bool> SaveAsync() =>
			await _context.SaveChangesAsync() > 0;

	}
}
