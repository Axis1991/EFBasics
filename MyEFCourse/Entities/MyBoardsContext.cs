using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("My_Iteration_path");
                eb.Property(wi => wi.Priority).HasDefaultValue(1);
                eb.HasMany(w => w.Comments)
                  .WithOne(c => c.WorkItem)
                  .HasForeignKey(c => c.WorkItemId);

                eb.HasOne(w => w.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(w => w.AuthorId);

                eb.HasMany(w => w.Tags)
                .WithMany(t => t.WorkItems)
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(wit => wit.TagId),

                    w => w.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemId),

                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    });
                    
                eb.HasOne(s => s.State)
                .WithMany().HasForeignKey(s => s.StateId);

            });

            modelBuilder.Entity<Comment>(eb =>
                {
                    eb.Property(x => x.DateCreated).HasDefaultValueSql("getutcdate()");
                    eb.Property(x => x.DateUpdated).ValueGeneratedOnUpdate();
                    eb.HasOne(x => x.Author).WithMany( a=> a.Comments).HasForeignKey(x => x.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(u => u.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<State>(eb =>
            {
            eb.Property(st => st.States)
            .HasMaxLength(50).IsRequired();
            eb.HasData(
                new State() { Id = 1, States = "To do" },
                new State { Id = 2, States = "Doing"}, 
                new State { Id = 3, States = "Done"});
            });


        }
    }
}
