using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Section;
using MyPastelitos.Web.Models;
using MyPastelitos.Web.Services.Abstractions;
using MyPastelitos.Web.Services.Implementations;
using System.Diagnostics;

namespace MyPastelitos.Web.Controllers

     
{

    [Authorize]

    public class HomeController : Controller
    {

        private readonly INotyfService _notyfService;
        private readonly IHomeService _homeService;

        public HomeController(INotyfService notyf, IHomeService homeService)
        {
            _notyfService = notyf;
            _homeService = homeService;
        }


        [HttpGet]

        public async Task<IActionResult> Index([FromQuery] PaginationRequest request)
        {

            Response<PaginationResponse<SectionDTO>> response = await _homeService.GetSectionsAsync(request);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                //TODO: redirect to  CUSTOM error page
                return RedirectToAction(nameof(Error));
            }

            return View(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Section([FromRoute] Guid id, [FromQuery] PaginationRequest request)
        {

            Response<SectionDTO> response = await _homeService.GetSectionAsync(id, request);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                //TODO: redirect to  CUSTOM error page
                return RedirectToAction(nameof(Error));
            }

            return View(response.Result);
        }



        [HttpGet]
        public async Task<IActionResult> Recipe([FromRoute] Guid id)
        {

            Response<RecipeDTO> response = await _homeService.GetRecipeAsync(id);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                //TODO: redirect to  CUSTOM error page
                return RedirectToAction(nameof(Error));
            }

            return View(response.Result);
        }



        [AllowAnonymous]

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
