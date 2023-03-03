using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Models.Response
{
    public class Response
    {
        public int? Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public object Data { get; set; }
    }
}
