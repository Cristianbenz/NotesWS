using DB;
using Microsoft.EntityFrameworkCore;
using NotesApi.Models;
using NotesApi.Models.Request;

namespace NotesApi.Services
{
    public class NotesService : INotesService
    {
        private NotesContext _db;

        public NotesService(NotesContext context)
        {
            _db = context;
        }

        public Note? AddNote(NoteRequest noteModel)
        {
            /* 
                Add a new note into the db

                Parameters: object of type Noterequest

                Returns object of type Note or null
            */

            User? user = _db.Users.Where(user => user.Id == noteModel.UserId).FirstOrDefault();
            if (user == null) return null;
            Note oNote = new Note()
            {
                Title = noteModel.Title,
                Text = noteModel.Text,
                UserId = noteModel.UserId,
            };
            _db.Note.Add(oNote);
            _db.SaveChanges();
            return oNote;
        }

        public List<Note> GetNotes(int userId)
        {
            var userNotes = _db.Note.Where(note => note.UserId == userId).ToList();
            return userNotes;
        }

        public Note? GetNoteById(int id)
        {
            /* 
                Find a note by id

                Parameters: id (int)

                Returns object of type Note or null
            */

            var oNote = _db.Note.Where(note => note.UserId == id).FirstOrDefault();
            return oNote;
        }

        public Note? Modify(UpdateNoteRequest updateModel, int noteId)
        {
            /* 
                Modify an existent note

                Parameters: object of type UpdateNoteRequest, noteId (int)

                Returns object of type Note or null
            */

            var oNote = _db.Note.Where(note => note.Id == noteId).FirstOrDefault();
            if (oNote == null) return null;
            oNote.Title = updateModel.Title;
            oNote.Text = updateModel.Text;
            oNote.StatusType = updateModel.StatusType;
            _db.Note.Update(oNote);
            _db.SaveChanges();

            return oNote;
        }
    }
}
