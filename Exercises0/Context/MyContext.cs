using Exercises0.Models;                    // used to call Employee models (Models -> Employee.cs)
using Microsoft.EntityFrameworkCore;        // used to implement DbContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercises0.Context
{
    public class MyContext : DbContext      // implement DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { 
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasMany(e => e.Educations)
                .WithOne(u => u.University);

            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Account)
                .WithOne(acc => acc.Employee)
                .HasForeignKey<Account>(acc_fk => acc_fk.NIK);

            modelBuilder.Entity<Account>()
                .HasOne(acc => acc.Profiling)
                .WithOne(p => p.Account)
                .HasForeignKey<Profiling>(p_fk => p_fk.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(e => e.Profilings)
                .WithOne(p => p.Education);

            modelBuilder.Entity<Account>()
                .HasMany(acr => acr.AccountRoles)
                .WithOne(acc => acc.Account);

            modelBuilder.Entity<Role>()
                .HasMany(acr => acr.AccountRoles)
                .WithOne(r => r.Role);
        }
    }
}
