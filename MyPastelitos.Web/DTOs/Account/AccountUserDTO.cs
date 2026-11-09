using Microsoft.AspNetCore.Mvc.Rendering;
using MyPastelitos.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Account
{
    public class AccountUserDTO
    {
        internal string FirstName;
        internal string Phone;
        internal string Document;

        public Guid? Id { get; internal set; }
        public string LastName { get; internal set; }
        public string Photo { get; internal set; }

        public class UserDTO
        {
            [Required(ErrorMessage = "El campo {0} es requerido")]

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
            [MaxLength(16, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres")]
            [Display(Name = "Telefono")]
            public required string Phone { get; set; }

            public required string? Email { get; set; }


            public string? PrivateRecipeRole { get; set; }

      
            public string? Photo { get; set; }

        }
    }
}
