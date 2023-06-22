﻿using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Context
{
    public class ServiceContext:DbContext
    {
        public ServiceContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserAddress> UsersAddresses { get; set; }

       
    }
}
