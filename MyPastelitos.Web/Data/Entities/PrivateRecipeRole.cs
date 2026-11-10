using MyPastelitos.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.Data.Entities
{
    public class PrivateRecipeRole : IID
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(32)]
        public required string Name { get; set; }

        public IEnumerable<RolePermission>? RolePermissions { get; set; }

    }
}
