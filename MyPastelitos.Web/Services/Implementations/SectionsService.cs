using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;
using AutoMapper;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MyPastelitos.Web.Services.Implementations
{
    public class SectionsService : CustomQueryableOperationsService, ISectionsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionsService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Response<CreateSectionDTO>> CreateSection(CreateSectionDTO dto)
        {
            return await CreateAsync<CreateSectionDTO, Section>(dto);
        }

        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Section>(id);
        }

        public async Task<Response<SectionDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<SectionDTO, Section>(id);
        }

        public async Task<Response<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            IQueryable<Section> query = _context.Sections.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
               
                query = query.Where(s => s.Name.ToLower().Contains(request.Filter.ToLower())
                                      || s.Description.ToLower().Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<SectionDTO, Section>(request, query);
        }

        public async Task<Response<object>> ToggleAsync(ToggleSectionStatusDTO dto)
        {
            try
            {
                Section? section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == dto.SectionId);

                if (section == null)
                {
                    return Response<object>.Failure($"No existe Seccion con id {dto.SectionId}");
                }

                section.IsHidden = dto.Hide;
                _context.Sections.Update(section);
                await _context.SaveChangesAsync();

                return Response<object>.Success($"Seccion con id {dto.SectionId} actualizada correctamente");

            }
            catch (Exception ex)
            {
                return Response<object>.Failure(ex);
            }
        }

        public async Task<Response<SectionDTO>> UpdateAsync(UpdateSectionDTO dto)
        {
            Response<UpdateSectionDTO> result = await UpdateAsync<UpdateSectionDTO, Section>(dto, dto.Id);

            SectionDTO dtoResponse = new SectionDTO
            {
                Id = result.Result.Id,
                Name = result.Result.Name,
                Description = result.Result.Description,
                IsHidden = result.Result.IsHidden
            };

            return Response <SectionDTO>.Success(dtoResponse, result.Message);
        }
    }
}
