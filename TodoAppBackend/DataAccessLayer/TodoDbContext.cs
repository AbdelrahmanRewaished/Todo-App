using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccessLayer
{
    public class TodoDbContext : IdentityDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) 
        {
            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreater != null)
            {
                // Create Database 
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                // Create Tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base method to inherit Identity model configuration
            base.OnModelCreating(modelBuilder);

            // Use HasMany and WithOne methods to define one-to-many relationship between ApplicationUser and Todo entity types
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Todos)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            const string CLIENT_ROLE_ID = "c0b9f977-2e0b-4aee-b751-5e39c6b31e35";
            const string ADMIN_ROLE_ID = "f0a0a7b2-3b6c-4c08-8e07-d48d265bd724";

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = CLIENT_ROLE_ID, Name = "Client", NormalizedName = "CLIENT" },
                new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
    }
}

