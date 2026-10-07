using System.ComponentModel.DataAnnotations;

namespace MyPastelitos.Web.DTOs.Section
{
    public class SectionDTO
    {
        public Guid Id { get; set; } 

        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsHidden { get; set; } = false;
    }
}
