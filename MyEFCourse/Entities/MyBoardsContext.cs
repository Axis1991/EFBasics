using Microsoft.EntityFrameworkCore;
using MyEFCourse.Entities.Config;
using MyEFCourse.Entities.Viewmodels;
using System.Net.Http.Headers;

namespace MyEFCourse.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
        { 

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Epic> Epics { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TopBoy> TopBoysView { get; set; }

        public DbSet<WorkItemTag> WorkItemTag { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// one by one Option 
            //new AddressConfig().Configure(modelBuilder.Entity<Address>());
            //new StateConfig().Configure(modelBuilder.Entity<State>());
            //new AddressConfig().Configure(modelBuilder.Entity<Address>());

            //more optimal option
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<Epic>()
            .Property(wi => wi.EndDate).HasPrecision(3);

            modelBuilder.Entity<Issue>()
            .Property(wi => wi.Effort).HasColumnType("decimal(5,2)");

            modelBuilder.Entity<Task>(task =>
            {
                task.Property(wi => wi.Activity).HasMaxLength(200);
                task.Property(wi => wi.RemainingWork).HasPrecision(14, 2);
            });


            modelBuilder.Entity<Comment>(eb =>
                {
                    eb.Property(x => x.DateCreated).HasDefaultValueSql("getutcdate()");
                    eb.Property(x => x.DateUpdated).ValueGeneratedOnUpdate();
                    eb.HasOne(x => x.Author).WithMany( a=> a.Comments).HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.ClientCascade);
                });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(a => a.UserId);



            modelBuilder.Entity<Tag>(t =>
            {
              t.HasData(
                    new Tag() { Id = 1, Value = "Web" },
                    new Tag { Id = 2, Value = "UI" },
                    new Tag { Id = 3, Value = "Desktop" },
                    new Tag { Id = 4, Value = "API" },
                    new Tag { Id = 5, Value = "Service" });
            });

            modelBuilder.Entity<TopBoy>(eb =>
                {
                    eb.ToView("View_TopBoys");
                    eb.HasNoKey();
                });

        }
    }
}
