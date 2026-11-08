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
              
    }
}
