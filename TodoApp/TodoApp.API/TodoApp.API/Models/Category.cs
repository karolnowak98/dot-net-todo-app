namespace TodoApp.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}