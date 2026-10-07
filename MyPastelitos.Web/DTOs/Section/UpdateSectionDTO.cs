using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Section
{
    public class UpdateSectionDTO
    {

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(32, ErrorMessage = "El campo {0} no puede exceder de 32 caracteres")]
        public string Name { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(128, ErrorMessage = "El campo {0} no puede exceder de 128 caracteres")]
        public string? Description { get; set; }

        public bool IsHidden { get; set; } 







}
}
