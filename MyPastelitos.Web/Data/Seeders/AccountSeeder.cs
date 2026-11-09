using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Data.Seeders
{
    public class AccountSeeder
    {
        private readonly IUsersService _usersService;
        private readonly IAccountService _accountService;
        private readonly DataContext _context;

        private const string BAKER_ROLE_NAME = "Panadero";
        private const string BASIC_ROLE_NAME = "Basic";

        public AccountSeeder(IUsersService usersService, DataContext context, IAccountService accountService)
        {
            _usersService = usersService;
            _context = context;
            _accountService = accountService;
        }

        public async Task SeedAsync()
        {
            await CheckRolesAsync();
            await CheckUserAsync();
        }

        private async Task CheckRolesAsync()
        {
            await AdminRoleAsync();
            await BakerRoleAsync();
            await BasicRoleAsync();
        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.PrivateRecipeRoles.AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

            if (!exists)
            {
                PrivateRecipeRole role = new PrivateRecipeRole
                {
                    Id = Guid.NewGuid(),
                    Name = Env.SUPER_ADMIN_ROLE_NAME
                };

                await _context.PrivateRecipeRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
        private async Task BasicRoleAsync()
        {
            bool exists = await _context.PrivateRecipeRoles.AnyAsync(r => r.Name == BASIC_ROLE_NAME);

            if (!exists)
            {
                PrivateRecipeRole role = new PrivateRecipeRole
                {
                    Id = Guid.NewGuid(),
                    Name = BASIC_ROLE_NAME
                };

                await _context.PrivateRecipeRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
        }
        private async Task BakerRoleAsync()
        {
            bool exists = await _context.PrivateRecipeRoles
                .AnyAsync(r => r.Name == BAKER_ROLE_NAME);

            PrivateRecipeRole role;

            if (!exists)
            {
                role = new PrivateRecipeRole
                {
                    Id = Guid.NewGuid(),
                    Name = BAKER_ROLE_NAME
                };

                await _context.PrivateRecipeRoles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            else
            {
                role = await _context.PrivateRecipeRoles
                    .FirstAsync(r => r.Name == BAKER_ROLE_NAME);
            }

            List<Permission> permissions = await _context.Permissions
                .Where(p => p.Module == "Producto" || p.Module == "Receta")
                .ToListAsync();

            foreach (Permission permission in permissions)
            {
                bool permissionExists = await _context.RolePermissions
                    .AnyAsync(rp => rp.PrivateRecipeId == role.Id &&
                                   rp.PermisssionId == permission.Id);

                if (!permissionExists)
                {
                    await _context.RolePermissions.AddAsync(
                        new RolePermission
                        {
                            PermisssionId = permission.Id,
                            PrivateRecipeId = role.Id
                        });
                }
            }

            await _context.SaveChangesAsync();
        }
        private async Task CheckUserAsync()
        {

            await AdminUserAsync();
            await BakerUserAsync();
            await BasicUserAsync();

        }

        private async Task AdminUserAsync()
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "admin@MyPastelitos.com");


            if (user is null)
            {
                PrivateRecipeRole adminRole = await _context.PrivateRecipeRoles.FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                user = new User
                {

                    Email = "admin@MyPastelitos.com",
                    UserName = "admin@MyPastelitos.com",
                    FirstName = "Administrator",
                    LastName = "Admin",
                    PhoneNumber = "3000000001",
                    Document = "1111",
                    PrivateRecipeRoleId = adminRole.Id

                };

                await _usersService.AddUserAsync(user, "1234");

                string token = (await _accountService.GenerateConfirmationTokenAsync(user)).Result;

                await _accountService.ConfirmUserAsync(user, token);
            }
        }



        private async Task BakerUserAsync()
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "matiasa@mypastelitos.com");


            if (user is null)
            {
                PrivateRecipeRole role = await _context.PrivateRecipeRoles.FirstOrDefaultAsync(r => r.Name == BAKER_ROLE_NAME);

                user = new User
                {

                    Email = "matiasa@MyPastelitos.com",
                    UserName = "matiasa@MyPastelitos.com",
                    FirstName = "Matias",
                    LastName = "Arboleda",
                    PhoneNumber = "3000000002",
                    Document = "2222",
                    PrivateRecipeRoleId = role.Id

                };

                await _usersService.AddUserAsync(user, "1234");

                string token = (await _accountService.GenerateConfirmationTokenAsync(user)).Result;

                await _accountService.ConfirmUserAsync(user, token);
            }
        }
        private async Task BasicUserAsync()
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "SantiagoG@mypastelitos.com");


            if (user is null)
            {
                PrivateRecipeRole role = await _context.PrivateRecipeRoles.FirstOrDefaultAsync(r => r.Name == BASIC_ROLE_NAME);

                user = new User
                {

                    Email = "santiagog@MyPastelitos.com",
                    UserName = "santiagog@MyPastelitos.com",
                    FirstName = "Santiago",
                    LastName = "Gomez",
                    PhoneNumber = "3000000003",
                    Document = "4444",
                    PrivateRecipeRoleId = role.Id

                };

                await _usersService.AddUserAsync(user, "1234");

                string token = (await _accountService.GenerateConfirmationTokenAsync(user)).Result;

                await _accountService.ConfirmUserAsync(user, token);
            }
        }
    }
}
