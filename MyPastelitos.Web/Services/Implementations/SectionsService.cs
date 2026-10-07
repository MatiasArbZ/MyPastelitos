using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;
using AutoMapper;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;

namespace MyPastelitos.Web.Services.Implementations
{
    public class SectionsService : CustomQueryableOperationsService, ISectionsService
    {
       private readonly DataContext _context;
       private readonly IMapper _mapper;

        public SectionsService(DataContext context, IMapper mapper) :base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<Response<CreateSectionDTO>> CreateSection(CreateSectionDTO dto)
        {
            return await CreateAsync<CreateSectionDTO, Section>(dto);
        }

        public Task<Response<object>> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SectionDTO>> GetOneAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<object>> ToggleAsync(ToggleSectionStatusDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<SectionDTO>> UpdateAsync(UpdateSectionDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
