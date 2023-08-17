using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
