using Road23.WebAPI.Database;
using Road23.WebAPI.Models;

namespace Road23.WebAPI.Interfaces
{
	public interface INoteRepository
	{
		ICollection<Note> GetAllNotes();
		ICollection<Note> GetAllNotesFromDate(DateOnly date);



		Task<bool> AddNoteAsync(Note note);
		Task<bool> UpdateNoteAsync(Note note);
		Task<bool> DeleteNoteAsync(Note note);

	}
}
