using Microsoft.EntityFrameworkCore;

namespace MyEFCourse.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
        { 

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder); 
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HRQN0Q1;Initial Catalog=EFCourseDB;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
