namespace TodoApp.API.Models.Category.Dto
{
    public class CreateCategoryRequestDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}