using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [EmailAddress(ErrorMessage = "el campo {0} debe ser un Email valido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "el campo {0} es requerido")]
        [MinLength(4, ErrorMessage = "el campo {0} debe tener por lo menos {1} caracteres")]
        [Display(Name = "contraseña")]
        public required string Password { get; set; }
    }
}
