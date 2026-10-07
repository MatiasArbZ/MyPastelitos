using System.ComponentModel.DataAnnotations;
namespace MyPastelitos.Web.DTOs.Section
{
    public class ToggleSectionStatusDTO
    {
        [Required (ErrorMessage = "El campo {0} es requerido")]
        public Guid Id { get; set; }

        public bool Hide { get; set; } = true;
    }
}
