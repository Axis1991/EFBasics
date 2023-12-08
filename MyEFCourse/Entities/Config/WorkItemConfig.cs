using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyEFCourse.Entities.Config
{
    public class WorkItemConfig : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> eb)
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

        }
    }
}
