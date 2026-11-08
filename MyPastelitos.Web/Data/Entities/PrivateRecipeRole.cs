using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.Data.Entities
{
    public class PrivateRecipeRole
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(32)]
        public required string Name { get; set; }

    }
}
