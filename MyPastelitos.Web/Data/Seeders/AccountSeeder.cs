using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        public AccountSeeder(IUsersService usersService, DataContext context, IAccountService accountService)
        {
            _usersService = usersService;
            _context = context;
            _accountService = accountService;
        }

        public async Task SeedAsync()
        {
            await AdminRoleAsync();
            await CheckUserAsync();
        }

        private async Task checkRolesAsync()
        {
            await AdminRoleAsync();
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

        private async Task CheckUserAsync()
        {
            //admin
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "admin@privaterecipe.com");


            if (user is null)
            {
                PrivateRecipeRole adminRole = await _context.PrivateRecipeRoles.FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                user = new User
                {

                    Email = "admin@privaterecipe.com",
                    UserName = "admin@privaterecipe.com",
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
    }
}
