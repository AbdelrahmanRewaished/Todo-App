using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }


        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
