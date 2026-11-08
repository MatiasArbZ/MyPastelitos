using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersService _userService;



        public SeedDb(DataContext context, IUsersService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task SeedAsync()
        {
            await new SectionsSeeder(_context).SeedAsyc();
            await new AccountSeeder(_userService, _context).SeedAsync();
        }


    }
}
