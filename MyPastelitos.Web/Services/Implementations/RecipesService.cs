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
    public class RecipesService :CustomQueryableOperationsService , IRecipesService
    {
        private readonly DataContext _context;

        public RecipesService(DataContext context, IMapper mapper):base(context, mapper)
        {
            _context = context;
        }
        


        public async Task<Response<CreateRecipeDTO>> CreateRecipe(CreateRecipeDTO dto)
        {
            return await CreateAsync<CreateRecipeDTO, Recipe> (dto); 

        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Recipe>(id);
        }

        public async Task<Response<RecipeDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<RecipeDTO, Recipe>(id);
        }

        public async Task<Response<PaginationResponse<RecipeDTO>>> GetPaginationAsync(PaginationRequest request)
        {

            IQueryable<Recipe> query = _context.Recipe.Include(b => b.Section)
                                                      .Select(b => new Recipe
                                                       {
                                                           Id = b.Id,
                                                           Name = b.Name,
                                                           Section = new Section
                                                           {
                                                               Id = b.Section.Id,
                                                               Name = b.Section.Name
                                                           }
                                                       })
                                                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {

                query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<RecipeDTO, Recipe>(request, query);
        }

        public async Task<Response<RecipeDTO>> UpdateAsync(UpdateRecipeDTO dto)
        {

            Response<UpdateRecipeDTO> result = await UpdateAsync<UpdateRecipeDTO, Recipe>(dto, dto.Id);

            RecipeDTO dtoResponse = new RecipeDTO
            {
                Id = result.Result.Id,
                Name = result.Result.Name,
                Description = result.Result.Description,
                Section = result.Result.Section,
                SectionId = result.Result.SectionId
            };

            return Response<RecipeDTO>.Success(dtoResponse, result.Message);
        }
    }
}
