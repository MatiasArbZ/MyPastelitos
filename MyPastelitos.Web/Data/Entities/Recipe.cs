using MyPastelitos.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPastelitos.Web.Data.Entities
{
    public class Recipe : IID
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(128)]
        [Required]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(MAX)")]
        public string? Description { get; set; } = null!;

        public Guid SectionId { get; set; }

        public Section? Section { get; set; }




    }
}
