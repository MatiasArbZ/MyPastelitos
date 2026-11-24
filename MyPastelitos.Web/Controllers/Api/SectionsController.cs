using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SectionsController : ApiController
    {
        private readonly ISectionsService _sectionsService;

        public SectionsController(ISectionsService sectionsService)
        {
            _sectionsService = sectionsService;
        }
        [HttpGet]
        [ApiAuthorize("sections.show")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
        {
            Response<PaginationResponse<SectionDTO>> response = await _sectionsService.GetPaginationAsync(request);
            return ControllerBasicValidation(response);
        }

        [HttpPost]
        [ApiAuthorize("sections.create")]
        public async Task<IActionResult> Create([FromBody] CreateSectionDTO dto)
        {
            Response<CreateSectionDTO> response = await _sectionsService.CreateSection(dto);
            return ControllerBasicValidation(response);
        }

        [HttpPut]
        [ApiAuthorize("sections.edit")]
        public async Task<IActionResult> Edit([FromForm] UpdateSectionDTO dto)
        {
            Response<SectionDTO> response = await _sectionsService.UpdateAsync(dto);
            return ControllerBasicValidation(response);
        }

        [HttpDelete("{id}")]
        [ApiAuthorize("sections.delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response response = await _sectionsService.DeleteAsync(id);
            return ControllerBasicValidation(response);
        }
    }
}

