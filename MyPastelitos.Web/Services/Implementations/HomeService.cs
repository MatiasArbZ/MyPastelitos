using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;
using ClaimsUser = System.Security.Claims.ClaimsPrincipal;

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



        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<RecipeDTO>> GetRecipeAsync(Guid id)
        {
            Recipe? recipe = await _context.Recipe.Include(r => r.Section)
                                                  .ThenInclude(s => s.RoleSection)
                                                  .FirstOrDefaultAsync(r => r.Id == id);
            if (recipe is null)
            {
                return Response<RecipeDTO>.Failure(new Exception("La receta con id: '{id}' no existe."));
            }

            ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
            ///////////////// TODO: cambiar identitiy
            string? userName = claimsUser.Identity.Name;
            Response<User> userResponse = await _usersService.GetUserByEmailAsync(userName);

            User user = userResponse.Result;

            bool isAutorized = true;
            if (!await _accountService.AccountUserIsSuperAdminAsync())
            {
                isAutorized = recipe.Section.RoleSection.Any(rs => rs.PrivateRecipeRoleId == user.PrivateRecipeRoleId);

            }

            if (!isAutorized)
            {
                return Response<RecipeDTO>.Failure("No tiene autroizacion para consultar esa receta");
            }

            RecipeDTO dto = _mapper.Map<RecipeDTO>(recipe);
            return Response<RecipeDTO>.Success(dto);

        }

        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<SectionDTO>> GetSectionAsync(Guid id, PaginationRequest request)
        {
            try
            {
                Section? section = await _context.Sections.Include(s => s.RoleSection)
                                                             .Where(s => !s.IsHidden && s.Id == id)
                                                                .FirstOrDefaultAsync();
                if (section is null)
                {
                    return Response<SectionDTO>.Failure(new Exception("El producto con id: '{id}' no existe."));
                }

                ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
                ///////////////// TODO: cambiar identitiy
                string? userName = claimsUser.Identity.Name;
                Response<User> userResponse = await _usersService.GetUserByEmailAsync(userName);

                User user = userResponse.Result;

                bool isAutorized = true;
                if (!await _accountService.AccountUserIsSuperAdminAsync())
                {
                    isAutorized = section.RoleSection.Any(rs => rs.PrivateRecipeRoleId == user.PrivateRecipeRoleId);

                }
                if (!isAutorized)
                {
                    return Response<SectionDTO>.Failure("No tiene autroizacion para consultar ese producto");
                }

                IQueryable<Recipe> query = _context.Recipe.Where(r => r.SectionId == section.Id);

                query = query.Select(r => new Recipe
                {
                    Id = r.Id,
                    Name = r.Name,
                    SectionId = r.SectionId,
                });


                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    query = query.Where(s => s.Name.Contains(request.Filter.ToLower()));
                }

                Response <PaginationResponse<RecipeDTO>> paginationResponse = await GetPagedListAsync<RecipeDTO, Recipe>(request, query);
                if(!paginationResponse.IsSuccess)
                {
                    return Response<SectionDTO>.Failure(paginationResponse.Message, paginationResponse.Errors);
                }

                SectionDTO dto = new SectionDTO
                {
                    Id = section.Id,
                    Name = section.Name,
                    Description = section.Description,
                    Price = section.Price,
                    PagedRecipes = paginationResponse.Result
                };

                return Response<SectionDTO>.Success(dto);
            }
            
                
            catch (Exception ex)

            {
                return Response<SectionDTO>.Failure(ex);
            }
        }

        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<PaginationResponse<SectionDTO>>> GetSectionsAsync(PaginationRequest request)
        {
            ClaimsUser? claimsUser = _httpContextAccessor.HttpContext?.User;
            /////////////////cambiar identitiy
            string? userName = claimsUser.Identity.Name;
            Response<User> userResponse = await _usersService.GetUserByEmailAsync(userName);

            User user = userResponse.Result;

            IQueryable<Section> query = _context.Sections.Include(s => s.RoleSection)
                                                         .Where(s => !s.IsHidden);

            if(!await _accountService.AccountUserIsSuperAdminAsync())
            {
                query = query.Where(s => s.RoleSection.Any(rs => rs.PrivateRecipeRoleId == user.PrivateRecipeRoleId));
            }

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(s => s.Name.Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<SectionDTO, Section>(request, query);
        }
    }
}


