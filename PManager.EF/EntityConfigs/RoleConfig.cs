using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManager.Domain.Models;

namespace PManager.EF.EntityConfigs
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedNever();
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Details)
                .HasMaxLength(500);
        }
    }
}
