using DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models.Request;
using NotesApi.Models.Response;
using NotesApi.Services;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NoteController : ControllerBase
    {
        private INotesService _noteService;

        public NoteController(INotesService notesService)
        {
            _noteService = notesService;
        }

        [HttpGet("{userId}")]
        [Authorize]
        public IActionResult GetAll(int userId)
        {
            /* 
                Search all notes asociated to the userId

                Parameters: Id of an user (int)
            */

            Response response = new Response();
            var notes = _noteService.GetNotes(userId);

            if (notes.Count == 0)
            {
                response.Message = "Not exist any register";
                return NotFound(response);
            }

            response.Success = 1;
            response.Data = notes;
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNote([FromBody]NoteRequest noteModel)
        {
            /* 
                Create a new note

                Parameters: object of type NoteRequest
            */

            Response response = new Response();
            try
            {
                Note? result = _noteService.AddNote(noteModel);
                if (result == null) return NotFound("User not exists");

                response.Success = 1;
                return Created("Note created successfuly", response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{noteId}")]
        [Authorize]
        public IActionResult EditNote([FromBody] UpdateNoteRequest requestModel, int noteId)
        {
            /* 
                Update a specific note

                Parameters: object of type UpdateNoteRequest
            */

            Response response = new Response();
            try
            {
                Note? updatedNote = _noteService.Modify(requestModel, noteId);
                if (updatedNote == null)
                {
                    response.Message = "Note not found";
                    return NotFound(response);

                }
                response.Success = 1;
                response.Data = updatedNote;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}