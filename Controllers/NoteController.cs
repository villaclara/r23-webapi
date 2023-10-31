using Microsoft.AspNetCore.Mvc;
using Road23.WebAPI.Interfaces;
using Road23.WebAPI.Models;
using Road23.WebAPI.Repository;
using Road23.WebAPI.Utility;
using Road23.WebAPI.Utility.ExtensionMethods;
using Road23.WebAPI.ViewModels;

namespace Road23.WebAPI.Controllers 
{
	[ApiController]
	[Route("api/[controller]")]
	public class NoteController : ControllerBase
	{
		private readonly INoteRepository _noteRepository;
		public NoteController(INoteRepository noteRepository)
		{
			_noteRepository = noteRepository;
		}

		[HttpGet]
		[ProducesResponseType(200)]
		public IActionResult GetAllNotes()
		{
			var notes = _noteRepository.GetAllNotes();

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			List<NoteVM> notesVM = new List<NoteVM>();
			foreach(var note in notes)
			{
				notesVM.Add(note.ConvertFromModel_ToVM());
			}

			return Ok(notesVM);
		}

		// Datetime represents DateTimeRouteConstraint Class
		[HttpGet("d={date:datetime}")]
		[ProducesResponseType(200)]
		public IActionResult GetNotesByDate(DateTime date)
		{
			var notes = _noteRepository.GetAllNotesFromDate(DateOnly.FromDateTime(date));

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			List<NoteVM> notesVM = new List<NoteVM>();
			foreach (var note in notes)
			{
				notesVM.Add(note.ConvertFromModel_ToVM());
			}

			return Ok(notesVM);
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> CreateExpense([FromBody] NoteVM noteToAdd)
		{
			// candle to Add is not set
			if (noteToAdd is null)
				return BadRequest(ModelState);

			// incorrect model state
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			// converting to model to add
			var note = noteToAdd.ConvertFromVM_ToModel();

			var isSuccess = await _noteRepository.AddNoteAsync(note);

			if (isSuccess == false)
			{
				ModelState.AddModelError("", "Internal error when creating note.");
				return StatusCode(500, ModelState);
			}

			return Ok(noteToAdd);

		}

		[HttpPut("eid={noteId:int}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> UpdateExpense([FromQuery] int noteId, [FromBody] NoteVM noteToUpdate)
		{
			if (noteToUpdate is null || noteToUpdate.Id != noteId)
				return StatusCode(400, ModelState);

			if (!ModelState.IsValid)
				return StatusCode(400, ModelState);

			var note = new Note
			{
				Id = noteId,
				IsDone = noteToUpdate.IsDone,
				NoteDate = noteToUpdate.NoteDate.ToDateTime(TimeOnly.Parse(ConstantsClass.DEFAULT_TIMEONLY_VALUE)),
				NoteText = noteToUpdate.NoteText,
			};


			var isSuccess = await _noteRepository.UpdateNoteAsync(note);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when updating note.");
				return StatusCode(500, ModelState);
			}

			return Ok(noteToUpdate);
		}


		[HttpDelete("eid={noteId:int}")]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[ProducesResponseType(200)]
		public async Task<IActionResult> DeleteExpense(int noteId)
		{
			var note = _noteRepository.GetAllNotes().Where(e => e.Id == noteId).FirstOrDefault();
			if (note is default(Note))
				return NotFound();

			var isSuccess = await _noteRepository.DeleteNoteAsync(note);
			if (!isSuccess)
			{
				ModelState.AddModelError("", "Internal error when deleting note.");
				return StatusCode(500, ModelState);
			}

			return Ok($"Expense - {noteId} deleted.");
		}

	}
}
