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
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<UserPermission>().ToTable("UserPermission");

            modelBuilder.Entity<UserPermission>()
                .HasKey(u => new { u.UserID, u.PermissionID });
        }
    }
}