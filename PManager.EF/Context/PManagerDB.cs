using Microsoft.EntityFrameworkCore;
using PManager.Domain.Models;
using PManager.EF.EntityConfigs;

namespace PManager.EF.Context
{
    class PManagerDB : DbContext
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Role> Roles { get; set; }

        public PManagerDB(DbContextOptions<PManagerDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());


            base.OnModelCreating(modelBuilder);
        }
    }
}
