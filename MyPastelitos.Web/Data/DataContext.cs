using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Data.Entities;

namespace MyPastelitos.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Section> Sections { get; set; }
    }
}
