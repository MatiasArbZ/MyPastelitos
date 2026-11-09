using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyPastelitos.Web.Data.Entities;

namespace MyPastelitos.Web.Data
{
    public class DataContext : IdentityDbContext<User>  
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Recipe> Recipe { get; set; }

        public DbSet<PrivateRecipeRole> PrivateRecipeRoles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ConfigureKeys(builder);
            ConfigureIdexes(builder);

            base.OnModelCreating(builder);
        }
        private void ConfigureKeys(ModelBuilder builder)
        {
                        // Role permission
            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.PrivateRecipeId, rp.PermisssionId });
            
            builder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermisssionId);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.PrivateRecipeRole)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.PrivateRecipeId);
        }
        private void ConfigureIdexes(ModelBuilder builder)
        {
            // Roles
            builder.Entity<PrivateRecipeRole>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // Sections
            builder.Entity<Section>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // User
            builder.Entity<User>()
                .HasIndex(u => u.Document)
                .IsUnique();

            // Permission
            builder.Entity<Permission>()
                .HasIndex(u => u.Code)
                .IsUnique();


        }

    }
}
