using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PManager.Domain.Models;

namespace PManager.EF.EntityConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder.Property(r => r.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.Password)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(r => r.FirstName)
                .HasMaxLength(50);
            builder.Property(r => r.SecondName)
                .HasMaxLength(50);
            builder.Property(r => r.Phone)
                .HasMaxLength(20);
        }
    }
}
