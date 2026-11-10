using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.DTOs.Recipe;
using MyPastelitos.Web.DTOs.Role;


namespace MyPastelitos.Web.Services.Abstractions
{
    public interface IRolesService
    {
        public Task<Response<RoleDTO>> CreateAsync (RoleDTO dto);

        public Task<Response> DeleteAsync(Guid id);
            
        public Task<Response<RoleDTO>> GetOneAsync(Guid id);

        public Task<Response<PaginationResponse<RoleDTO>>> GetPaginationAsync(PaginationRequest request);

        public Task<Response<RoleDTO>> UpdateAsync(RoleDTO dto);

        public Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync();

        
    }
}
