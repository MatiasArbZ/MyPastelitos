using MyPastelitos.Web.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.Data.Entities
{
    public class Section : IID
    {
            [Key]
            public Guid Id { get; set; }

            [MaxLength(32)]

            public required string Name { get; set;  }

            [MaxLength(128)]

            public string? Description { get; set; } 

            public bool IsHidden {  get; set; }
        }
    }

