using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManager.Domain.Models;

namespace PManager.EF.EntityConfigs
{
    public class AgencyConfig : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(r => r.Name)       .IsRequired().HasMaxLength(50);
            builder.Property(r => r.FullName)   .HasMaxLength(200);
            builder.Property(r => r.Details)    .HasMaxLength(500);
        }
    }
}
