using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [MaxLength(60)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string? LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Todo>? Todos { get; set; }
    }
}
