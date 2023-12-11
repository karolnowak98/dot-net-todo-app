namespace TodoApp.API.Models.Category;

public class Category
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType Type { get; set; }
}