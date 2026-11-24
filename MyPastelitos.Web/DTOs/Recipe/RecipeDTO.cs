using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Recipe
{
    public class RecipeDTO
    {
  
        public Guid Id { get; set; }

        public string Name { get; set; }
  
        public string? Description { get; set; }

    }
}
