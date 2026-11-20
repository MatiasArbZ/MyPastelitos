using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Attributes;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ApiController
    {
        private readonly ISectionsService _sectionsService;

        public SectionsController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }
        [HttpGet]

        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<SectionDTO>> response = await _sectionsService.GetPaginationAsync(request);
            return ControllerBasicValidation(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectionDTO dto)
        {
            Response<CreateSectionDTO> response = await _sectionsService.CreateSection(dto);
            return ControllerBasicValidation(response);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] UpdateSectionDTO dto)
        {
            Response<SectionDTO> response = await _sectionsService.UpdateAsync(dto);
            return ControllerBasicValidation(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response response = await _sectionsService.DeleteAsync(id);
            return ControllerBasicValidation(response);
        }
    }
}

