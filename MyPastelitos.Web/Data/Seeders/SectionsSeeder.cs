using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Data.Entities;

namespace MyPastelitos.Web.Data.Seeders
{
    public class SectionsSeeder
    {
        private readonly DataContext _context;

        public SectionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsyc()
        {
            List<Section> sections = new List<Section>
            {
                new Section { Id = Guid.NewGuid(), Name = "General", Description = "Informacion base de la compañía" },
                new Section { Id = Guid.NewGuid(), Name = "Informatica"},
                new Section { Id = Guid.NewGuid(), Name = "Pentesting"},
                new Section { Id = Guid.NewGuid(), Name = "Clases", IsHidden = true},
            };

            foreach(Section section in sections)
            {
                bool exists = await _context.Sections.AnyAsync(s => s.Name == section.Name);

                if (!exists)
                {
                    await _context.Sections.AddAsync(section);
                }

            }

            await _context.SaveChangesAsync();

                
        }
    }
}
