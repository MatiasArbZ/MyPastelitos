using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Data.Seeders
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersService _userService;
        private readonly IAccountService _accountService;



        public SeedDb(DataContext context, IUsersService userService, IAccountService accountService)
        {
            _context = context;
            _userService = userService;
            _accountService = accountService;
        }

        public async Task SeedAsync()
        {
            await new SectionsSeeder(_context).SeedAsyc();
            await new AccountSeeder(_userService, _context, _accountService).SeedAsync();
        }


    }
}
