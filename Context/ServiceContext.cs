using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Context
{
    public class ServiceContext:DbContext
    {
        public ServiceContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserAddress> UsersAddresses { get; set; }

        override
       protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user=>user.FirstName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user => user.LastName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();

            modelBuilder.Entity<Role>().HasIndex(role => role.RoleName).IsUnique();



        }
    }
}
