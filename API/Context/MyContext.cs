using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> LogActivities { get; set; }
        public DbSet<Division> Divisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(sc => sc.UserId);
            modelBuilder.Entity<Employee>().HasKey(sc => sc.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.userRoles)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<Employee>()
                .HasOne<User>(s => s.User)
                .WithOne(ad => ad.Employee)
                .HasForeignKey<Employee>(ad => ad.UserId);
        }
    }
}
