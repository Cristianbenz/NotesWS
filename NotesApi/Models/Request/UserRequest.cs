using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models.Request
{
    public class UserRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
