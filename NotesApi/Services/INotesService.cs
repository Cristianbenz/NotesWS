using DB;
using NotesApi.Models.Request;

namespace NotesApi.Services
{
    public interface INotesService
    {
        Note? AddNote(NoteRequest noteModel);

        List<Note> GetNotes(int userId);

        Note? GetNoteById(int id);

        Note? Modify(UpdateNoteRequest updateModel, int noteId);
    }
}
