using Microsoft.EntityFrameworkCore;
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

            
            [Precision(18, 2)]
            public decimal Price { get; set; }

            public bool IsHidden {  get; set; }
        }
    }

