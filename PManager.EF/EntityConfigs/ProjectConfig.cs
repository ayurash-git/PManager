using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManager.Domain.Models;

namespace PManager.EF.EntityConfigs
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(r => r.Name)       .IsRequired().HasMaxLength(50);
            builder.Property(r => r.Details)    .HasMaxLength(500);

            builder.Property(r => r.DateCreate) .HasColumnType("datetime2").HasPrecision(0);
            builder.Property(r => r.DateStart)  .HasColumnType("datetime2").HasPrecision(0);
            builder.Property(r => r.DateEnd)    .HasColumnType("datetime2").HasPrecision(0);
            builder.Property(r => r.DateDone)   .HasColumnType("datetime2").HasPrecision(0);

        }
    }
}
