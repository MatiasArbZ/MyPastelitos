using Microsoft.AspNetCore.Mvc.Rendering;
using MyPastelitos.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.User
{
    public class UserDTO
    {

        public Guid? Id { get; set; }
       
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(32, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Documento")]
        public required string Document { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Nombre")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Apellidos")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(64, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "el campo {0} debe ser un Email valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(16, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
        [Display(Name = "Telefono")]
        public required string Phone { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Rol")]
        public   Guid PrivateRecipeRoleId { get; set; }

        public PrivateRecipeRole? PrivateRecipeRole { get; set; }

        public IEnumerable<SelectListItem>? PrivateRecipeRoles { get; set; }


        public string? Photo { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
