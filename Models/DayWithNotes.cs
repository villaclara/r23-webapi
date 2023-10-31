namespace Road23.WebAPI.Models
{
	public class DayWithNotes
	{
		public int Id { get; set; }
		public DateOnly Date {  get; set; }
		public ICollection<Note> Notes { get; set; } = null!;
	}
}
