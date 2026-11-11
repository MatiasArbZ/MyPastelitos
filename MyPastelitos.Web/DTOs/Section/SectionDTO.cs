using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Recipe;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Section
{
    public class SectionDTO
    {
        public Guid Id { get; set; } 

        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsHidden { get; set; } = false;

        public PaginationResponse<RecipeDTO>? PagedRecipes { get; set; }
    }
}
