
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Api.DbContext
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {

        public AuthDbContext (DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        base.OnModelCreating(modelBuilder);
        // Configure Asp Net Identity Tables
        modelBuilder.Entity<IdentityUser>().ToTable("Users");
        modelBuilder.Entity<IdentityUser>().Property(u => u.Id).HasMaxLength(500);
        modelBuilder.Entity<IdentityUser>().Property(u => u.PasswordHash).HasMaxLength(500);
        modelBuilder.Entity<IdentityUser>().Property(u => u.UserName ).HasMaxLength(256);
        modelBuilder.Entity<IdentityUser>().Property(u => u.PhoneNumber).HasMaxLength(20);

        }
        
        public DbSet<IdentityUser> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        
    }
}