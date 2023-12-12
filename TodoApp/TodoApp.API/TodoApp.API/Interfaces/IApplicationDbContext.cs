using TodoApp.API.Models.Category;
using TodoApp.API.Models.TaskCategory;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Task> Tasks { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<TaskCategory> TaskCategories { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}