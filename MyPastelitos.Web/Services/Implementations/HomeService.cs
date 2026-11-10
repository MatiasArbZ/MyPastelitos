using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;
using ClaimUser = System.Security.Claims.ClaimsPrincipal;

namespace MyPastelitos.Web.Services.Implementations
{
    public class HomeService : CustomQueryableOperationsService, IHomeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public HomeService(DataContext context,IHttpContextAccessor httpContextAccessor, IAccountService accountService, IMapper mapper, IUsersService usersService) : base(context, mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
            _mapper = mapper;
            _usersService = usersService;
        }

        public Task<Response<RecipeDTO>> GetRecipeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SectionDTO>> GetSectionAsync(Guid id, PaginationRequest request)
        {
            throw new NotImplementedException();
        }

        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<PaginationResponse<SectionDTO>>> GetSectionsAsync(PaginationRequest request)
        {
            ClaimUser? claimsUser = _httpContextAccessor.HttpContext?.User;
            string? userName = ClaimUser.PrimaryIdentitySelector.Name;
           Response<User> userResponse = await _usersService.GetUserByEmailAsync(userName);

            User user = userResponse.Result;

            IQueryable<Section> query = _context.Sections.Include(s => s.RoleSection)
                                                         .Where(s => !s.IsHidden);

            if(!await _accountService.AccountUserIsSuperAdminAsync())
            {
                query = query.Where(s => s.RoleSection.Any(rs=> rs.PrivateRecipeRoleId == user.PrivateRecipeRoleId))
            }

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(s => s.Name.Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<SectionDTO, Section>(request, query);
        }
    }
}
