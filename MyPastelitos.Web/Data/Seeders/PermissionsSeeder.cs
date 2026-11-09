using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Data.Entities;
namespace MyPastelitos.Web.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;

        }
        public async Task SeedAsync()
        {
            List<Permission> permissions = [.. Recipe(), .. Sections(), .. Users(), .. Roles()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Code == permission.Code);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }
            await _context.SaveChangesAsync();
        }


        private List<Permission> Recipe()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Code = "Recipe.show", Description = "Ver Receta", Module = "Receta"},
                new Permission { Id = Guid.NewGuid(), Code = "Recipe.create", Description = "Crear Receta", Module = "Receta"},
                new Permission { Id = Guid.NewGuid(), Code = "Recipe.update", Description = "Editar Receta", Module = "Receta"},
                new Permission { Id = Guid.NewGuid(), Code = "Recipe.delete", Description = "eliminar Receta", Module = "Receta"},
            };
        }
        private List<Permission> Sections()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Code = "sections.show", Description = "Ver Producto", Module = "Producto"},
                new Permission { Id = Guid.NewGuid(), Code = "sections.create", Description = "Crear Producto", Module = "Producto"},
                new Permission { Id = Guid.NewGuid(), Code = "sections.update", Description = "Editar Producto", Module = "Producto"},
                new Permission { Id = Guid.NewGuid(), Code = "sections.delete", Description = "eliminar Producto", Module = "Producto"},
            };
        }
        private List<Permission> Users()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Code = "users.show", Description = "Ver Usuarios", Module = "Usuarios"},
                new Permission { Id = Guid.NewGuid(), Code = "users.create", Description = "Crear Usuarios", Module = "Usuarios"},
                new Permission { Id = Guid.NewGuid(), Code = "users.update", Description = "Editar Usuarios", Module = "Usuarios"},
                new Permission { Id = Guid.NewGuid(), Code = "users.delete", Description = "eliminar Usuarios", Module = "Usuarios"},
            };
        }
        private List<Permission> Roles()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Code = "roles.show", Description = "Ver Roles", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Code = "roles.create", Description = "Crear Roles", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Code = "roles.update", Description = "EditarRoles ", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Code = "roles.delete", Description = "eliminar Roles", Module = "Roles"},
            };
        }

    }
    }

