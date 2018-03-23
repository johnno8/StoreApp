using StoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Data
{
    public class StoreAppContext : DbContext
    {
        public StoreAppContext(DbContextOptions<StoreAppContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserPermission>().ToTable("UserPermission");
            modelBuilder.Entity<Permission>().ToTable("Permission");
        }
    }
}