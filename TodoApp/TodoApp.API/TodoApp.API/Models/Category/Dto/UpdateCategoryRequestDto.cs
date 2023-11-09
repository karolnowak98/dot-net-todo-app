namespace TodoApp.API.Models.Category.Dto
{
    public class UpdateCategoryRequestDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}