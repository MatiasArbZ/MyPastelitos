using System.ComponentModel.DataAnnotations;
using pbSection = MyPastelitos.Web.Data.Entities.Section;

namespace MyPastelitos.Web.DTOs.Recipe
{
    public class RecipeDTO
    {
  
        public Guid Id { get; set; }

        public string Name { get; set; }
  
        public string? Description { get; set; }

        public Guid SectionId { get; set; }

        public pbSection? Section { get; set; }
    }
}
