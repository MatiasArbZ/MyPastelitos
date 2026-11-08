using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Recipe;

namespace MyPastelitos.Web.Services.Abstractions
{
    public interface IRecipesService
    {
       
        public Task<Response<CreateRecipeDTO>> CreateRecipe(CreateRecipeDTO dto);

        public Task<Response> DeleteAsync(Guid id);

        public Task<Response<RecipeDTO>> GetOneAsync(Guid id);

        public Task<Response<PaginationResponse<RecipeDTO>>> GetPaginationAsync(PaginationRequest request);

        public Task<Response<RecipeDTO>> UpdateAsync(UpdateRecipeDTO dto);
       
    }
}
