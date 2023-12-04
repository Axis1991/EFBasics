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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkItem>()
                .Property(x => x.State)
                .IsRequired();

            modelBuilder.Entity<WorkItem>()
                .Property(x => x.Area)
                .HasColumnType("varchar(200)");

            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.IterationPath).HasColumnName("My_Iteration_path");
                eb.Property(wi => wi.Effort).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemainingWork).HasPrecision(14,2);
                eb.Property(wi => wi.Priority).HasDefaultValue(1);

            });

            modelBuilder.Entity<Comment>(eb =>
                {
                    eb.Property(x => x.DateCreated).HasDefaultValueSql("getutcdate()");
                    eb.Property(x => x.DateUpdated).ValueGeneratedOnUpdate();
                 });
             
    }
}
