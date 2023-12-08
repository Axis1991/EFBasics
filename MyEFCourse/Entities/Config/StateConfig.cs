using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MyEFCourse.Entities.Config
{
        public class StateConfig : IEntityTypeConfiguration<State>
        {
            public void Configure(EntityTypeBuilder<State> builder)
            {
                builder.Property(st => st.States)
                     .HasMaxLength(50).IsRequired();
                
                builder.HasData(
                    new State() { Id = 1, States = "To do" },
                    new State { Id = 2, States = "Doing" },
                    new State { Id = 3, States = "Done" });
            }
        }
}
