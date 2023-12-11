using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using Task = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.DAL;

public class TasksDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

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