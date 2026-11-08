using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace MyPastelitos.Web.DTOs.Recipe
{
    public class CreateRecipeDTO
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(32, ErrorMessage = "El campo {0} no puede exceder de 32 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripcion")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display (Name = "Seccion")]
        public Guid SectionId { get; set; }

        public Data.Entities.Section? Section { get; set;}

        public List<SelectListItem>? Sections { get; set; }
    }
}
