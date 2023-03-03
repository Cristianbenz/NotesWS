using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models.Request
{
    public class SignUpRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
    }
}
