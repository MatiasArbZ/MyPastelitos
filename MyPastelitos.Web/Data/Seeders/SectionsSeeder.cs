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
