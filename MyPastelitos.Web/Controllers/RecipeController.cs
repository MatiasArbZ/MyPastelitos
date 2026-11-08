using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Helpers.Abstractions;
using MyPastelitos.Web.Services.Abstractions;
using MyPastelitos.Web.Services.Implementations;

namespace MyPastelitos.Web.Controllers
{

    [Authorize]
    public class RecipeController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly IRecipesService _recipesService;
        private readonly ISelectListHelper _selectListHelper;
       


        public RecipeController(INotyfService notyfService, IRecipesService recipesService, ISelectListHelper selectListHelper)
        {
            _notyfService = notyfService;
            _recipesService = recipesService;
            _selectListHelper = selectListHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {

            Response<PaginationResponse<RecipeDTO>> response = await _recipesService.GetPaginationAsync(request);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction("Index", "Home");
            }

            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateRecipeDTO dto = new CreateRecipeDTO
            {
                Sections = await _selectListHelper.GetSectionsSelectListAsync()
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateRecipeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validacion");
                dto.Section = await _selectListHelper.GetSectionsSelectListAsync();
                return View(dto);
            }

            Response<CreateRecipeDTO> response = await _recipesService.CreateRecipe(dto);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                dto.Section = await _selectListHelper.GetSectionsSelectListAsync();
                return View(dto);
            }

            _notyfService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            Response<RecipeDTO> response = await _recipesService.GetOneAsync(id);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            UpdateRecipeDTO dto = new UpdateRecipeDTO
            {
                Id = response.Result.Id,
                Name = response.Result.Name,
                Description = response.Result.Description,
                SectionId = response.Result.SectionId,
                Sections = await _selectListHelper.GetSectionsSelectListAsync()
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] UpdateRecipeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validacion");
                dto.Section = await _selectListHelper.GetSectionsSelectListAsync();
                return View(dto);
            }


            Response<RecipeDTO> response = await _recipesService.UpdateAsync(dto);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                dto.Section = await _selectListHelper.GetSectionsSelectListAsync();
                return View(dto);
            }

            _notyfService.Success(response.Message);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response response = await _recipesService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);

            }
            else
            {
                _notyfService.Success(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
