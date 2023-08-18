using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegistrationModel
    {
        [Required]
        [MaxLengthAttribute(60)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLengthAttribute(60)]
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
