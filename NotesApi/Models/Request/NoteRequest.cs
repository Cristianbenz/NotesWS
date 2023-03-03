namespace NotesApi.Models.Request
{
    public class NoteRequest
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
