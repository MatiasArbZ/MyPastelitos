using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(32)]
        public required string Document { get; set; }

        [Required]
        [MaxLength(64)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public required string LastName { get; set; }

        public required Guid PrivateRecipeRoleId { get; set; }

        public PrivateRecipeRole? PrivateRecipeRole { get; set; }

        public string? Photo { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
