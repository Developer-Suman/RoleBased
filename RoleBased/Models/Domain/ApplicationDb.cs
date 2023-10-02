using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RoleBased.Models.Domain
{
    public class ApplicationDb :  IdentityDbContext<ApplicationUser>
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options) 
        {
            
        }

        public DbSet<Material> Materials { get; set; }
        //public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Categories> Categories { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Material>()
        //        .HasOne(m => m.User)
        //        .WithMany() // If you want a one-to-many relationship
        //        .HasForeignKey(m => m.UserId);
        //}
    }
}
