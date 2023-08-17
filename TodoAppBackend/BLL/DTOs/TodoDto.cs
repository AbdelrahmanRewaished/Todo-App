namespace BusinessLogicLayer.DTOs
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
