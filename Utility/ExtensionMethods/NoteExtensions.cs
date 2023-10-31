using Road23.WebAPI.Models;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Utility.ExtensionMethods
{
	public static class NoteExtensions
	{
		public static NoteVM ConvertFromModel_ToVM(this Note note) =>
			new()
			{
				Id = note.Id,
				IsDone = note.IsDone,
				NoteDate = DateOnly.FromDateTime(note.NoteDate),
				NoteText = note.NoteText,
			};

		public static Note ConvertFromVM_ToModel(this NoteVM noteVM) =>
			new()
			{
				Id = noteVM.Id,
				IsDone = noteVM.IsDone,
				NoteDate = noteVM.NoteDate.ToDateTime(TimeOnly.Parse(ConstantsClass.DEFAULT_TIMEONLY_VALUE)),
				NoteText = noteVM.NoteText,
			};

	}
}
