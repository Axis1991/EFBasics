using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyEFCourse.Entities.Config
{
        public class AddressConfig : IEntityTypeConfiguration<Address>
        {
            public void Configure(EntityTypeBuilder<Address> builder)
            {
                builder.OwnsOne(a => a.Coordinate, ownd =>
                {
                    ownd.Property(c => c.Latitude).HasPrecision(18, 7);
                    ownd.Property(c => c.Longitude).HasPrecision(18, 7);
                });
            }
        }
    
}
