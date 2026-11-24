using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Recipe
{
    public class UpdateRecipeDTO
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(32, ErrorMessage = "El campo {0} no puede exceder de 32 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripcion")]
        public string? Description { get; set; }

    }
}
