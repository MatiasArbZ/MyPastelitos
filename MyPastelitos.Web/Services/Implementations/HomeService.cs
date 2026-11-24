using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Services.Implementations
{
    public class HomeService : CustomQueryableOperationsService, IHomeService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public HomeService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<RecipeDTO>> GetRecipeAsync(Guid id)
        {
            Recipe? recipe = await _context.Recipe
                                           .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe is null)
            {
                return Response<RecipeDTO>.Failure(new Exception($"La receta con id: '{id}' no existe."));
            }

            RecipeDTO dto = _mapper.Map<RecipeDTO>(recipe);
            return Response<RecipeDTO>.Success(dto);
        }


        //TODO : trANSPONSE GET USERRESPONSABILITY TO IACCOUNTSERVICE
        public async Task<Response<SectionDTO>> GetSectionAsync(Guid id, PaginationRequest request)
        {
            try
            {
                Section? section = await _context.Sections
                    .Where(s => !s.IsHidden && s.Id == id)
                    .FirstOrDefaultAsync();
                if (section is null)
                {
                    return Response<SectionDTO>.Failure(new Exception("El producto con id: '{id}' no existe."));
                }


                     IQueryable<Recipe> query = _context.Recipe.AsQueryable();


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
            IQueryable<Section> query = _context.Sections
                                                .Where(s => !s.IsHidden);

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(s => s.Name.Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<SectionDTO, Section>(request, query);
        }
    }
}


