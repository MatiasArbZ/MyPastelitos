using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Section;

namespace MyPastelitos.Web.Services.Abstractions
{
    public interface ISectionsService

    {
        public Task<Response<CreateSectionDTO>> CreateSection(CreateSectionDTO dto);

        public Task<Response<object>> DeleteAsync(Guid id);

        public Task<Response<SectionDTO>> GetOneAsync(Guid id);

        public Task<Response<PaginationResponse<SectionDTO>>> GetPaginationAsync(PaginationRequest request);

        public Task<Response<SectionDTO>> UpdateAsync(SectionDTO dto);
        public Task<Response<object>> ToggleAsync(ToggleSectionStatusDTO dto);


        

    }
}
