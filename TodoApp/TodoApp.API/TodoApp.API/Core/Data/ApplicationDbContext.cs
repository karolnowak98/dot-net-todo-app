using TodoApp.API.Core.Data.Identity;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.TaskCategory;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Core.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
            
        builder.Entity<TaskCategory>().HasNoKey();
        builder.Entity<Category>().Property(e => e.Type).HasConversion<int>();
    }
        
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<TaskCategory> TaskCategories { get; set; }
}