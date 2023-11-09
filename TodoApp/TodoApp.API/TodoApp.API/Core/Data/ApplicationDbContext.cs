using TodoApp.API.Core.Models;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.Comment;
using TodoApp.API.Models.Note;
using TodoApp.API.Models.TaskCategory;
using TodoApp.API.Models.User;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<TaskCategory>().HasNoKey();
        }
        
        //public new DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }
    }
}