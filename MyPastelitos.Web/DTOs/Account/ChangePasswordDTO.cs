using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Account
{
    public class ChangePasswordDTO
    {

        [Display(Name = "Contraseña actual")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres como minimo.")]
        [Required (ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Nueva contraseña")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres como minimo.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Nueva Contraseña Confirmación")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres como minimo.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
        
    }

}

