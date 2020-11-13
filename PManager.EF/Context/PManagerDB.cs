using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models;
using PManager.EF.EntityConfigs;

namespace PManager.EF.Context
{
    public class PManagerDb : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Project> Projects { get; set; }

        public PManagerDb(DbContextOptions<PManagerDb> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new GenderConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new AgencyConfig());
            modelBuilder.ApplyConfiguration(new ProjectConfig());


            base.OnModelCreating(modelBuilder);
        }
    }
}
