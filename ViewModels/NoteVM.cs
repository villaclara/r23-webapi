namespace Road23.WebAPI.ViewModels
{
	public record class NoteVM
	{
		public int Id { get; set; }
		public bool IsDone { get; set; }
		public string NoteText { get; set; } = null!;
		public DateOnly NoteDate { get; set; }
	}
}
