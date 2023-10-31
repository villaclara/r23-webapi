namespace Road23.WebAPI.Models
{
	public class Note
	{
		public int Id { get; set; }
		public bool IsDone { get; set; }
		public string NoteText { get; set; } = null!;
		public DateTime NoteDate { get; set; }

	}
}
