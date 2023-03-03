namespace NotesApi.Models.Request
{
    public class UpdateNoteRequest
    {
        public string Text { get; set; }

        public string Title { get; set; }

        public int StatusType { get; set; }
    }
}
