namespace TodoApp.API.Models
{
    public class Task
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}