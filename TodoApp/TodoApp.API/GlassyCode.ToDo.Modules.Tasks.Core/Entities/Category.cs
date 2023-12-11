using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType Type { get; set; }
}