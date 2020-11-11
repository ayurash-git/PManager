using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManager.Domain.Models;

namespace PManager.EF.EntityConfigs
{
    public class JobConfig : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
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
