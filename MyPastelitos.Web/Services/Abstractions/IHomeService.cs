using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;

namespace MyPastelitos.Web.Services.Abstractions
{
    public interface IHomeService
    {
        public Task<Response<PaginationResponse<SectionDTO>>> GetSectionsAsync(PaginationRequest request);
        public Task<Response<SectionDTO>> GetSectionAsync(Guid id, PaginationRequest request);
        public Task<Response<RecipeDTO>> GetRecipeAsync(Guid id);
       
    }
}
