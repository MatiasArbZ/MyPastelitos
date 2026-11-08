using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPastelitos.Web.Helpers.Abstractions
{
    public interface ISelectListHelper
    {
        Task<List<SelectListItem>> GetSectionsSelectListAsync();
    }
}
