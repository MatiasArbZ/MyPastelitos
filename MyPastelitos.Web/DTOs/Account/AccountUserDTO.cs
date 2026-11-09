using Microsoft.AspNetCore.Mvc.Rendering;
using MyPastelitos.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Account
{
    public class AccountUserDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Documento")]
        public required string Document { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Nombres")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Apellidos")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(16, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Display(Name = "Teléfono")]
        public required string Phone { get; set; }

        public string? Email { get; set; }

        public string? RoleName { get; set; }

        public string? Photo { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
