using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Role;
using MyPastelitos.Web.Services.Abstractions;
using Newtonsoft.Json;

namespace MyPastelitos.Web.Services.Implementations
{
    public class RolesService : CustomQueryableOperationsService, IRolesService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RolesService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<RoleDTO>> CreateAsync(RoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
               

                    //role
                    PrivateRecipeRole role = _mapper.Map<PrivateRecipeRole>(dto);

                    await _context.PrivateRecipeRoles.AddAsync(role);

                    await _context.SaveChangesAsync();

                    //permissions

                    List<Guid> permissionIds = [];

                    if (!string.IsNullOrEmpty(dto.PermissionsIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionsIds);
                    }

                    foreach (Guid permissionId in permissionIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            PrivateRecipeRoleId = role.Id,
                            PermissionId = permissionId
                        };

                        await _context.RolePermissions.AddAsync(rolePermission);
                    }

                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return Response<RoleDTO>.Success(dto, "Rol creado exitosamente");

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Response<RoleDTO>.Failure(ex);
                }
            }
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
           if(await _context.Users.AnyAsync(u => u.PrivateRecipeRoleId == id))
            {
                return Response.Failure("No se puede eliminar el rol porque hay usuarios asociados a el");
            }

            return await DeleteAsync<PrivateRecipeRole>(id);
        }

        public async Task<Response<RoleDTO>> GetOneAsync(Guid id)
        {
            Response<RoleDTO> response = await GetOneAsync<RoleDTO,PrivateRecipeRole>(id);

            if (!response.IsSuccess)
            {
                return response;
            }

            RoleDTO dto = response.Result;

            List<PermissionsForRoleDTO> permissions = await _context.Permissions.Select(p => new PermissionsForRoleDTO
            {
                Id=p.Id,
                Description=p.Description,
                Module=p.Module,
                Code=p.Code,
                Selected = _context.RolePermissions.Any(rp => rp.PermissionId == p.Id && rp.PrivateRecipeRoleId == dto.Id)
            }).ToListAsync();

            dto.Permissions = permissions;
            return Response<RoleDTO>.Success(dto);

        }

        public async Task<Response<PaginationResponse<RoleDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            IQueryable<PrivateRecipeRole> query = _context.PrivateRecipeRoles.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                query = query.Where(r => r.Name.ToLower().Contains(request.Filter.ToLower()));
            }

            return await GetPagedListAsync<RoleDTO, PrivateRecipeRole>(request, query);
        }

        public async Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync()
        {
           Response <List<PermissionDTO>> permissionsResponse = await GetCompleteListAsync<PermissionDTO, Permission>();
            if (!permissionsResponse.IsSuccess)
            {
                return Response<List<PermissionsForRoleDTO>>.Failure(permissionsResponse.Message, permissionsResponse.Errors);
            }
            List<PermissionsForRoleDTO> permissions =  permissionsResponse.Result.Select(p => new PermissionsForRoleDTO
            {
                Id = p.Id,
                Description = p.Description,
                Module = p.Module,
                Code = p.Code,
                Selected = false,
            }).ToList();

            return Response<List<PermissionsForRoleDTO>>.Success(permissions);
        }

        public async Task<Response<RoleDTO>> UpdateAsync(RoleDTO dto)
        {
            try
            {
                if (dto.Name == Env.SUPER_ADMIN_ROLE_NAME)
                {
                    return Response<RoleDTO>.Failure(
                        "No se puede modificar el rol de super administrador");
                }

                // Buscar el rol existente
                PrivateRecipeRole? role = await _context.PrivateRecipeRoles
                    .FirstOrDefaultAsync(r => r.Id == dto.Id);

                if (role == null)
                {
                    return Response<RoleDTO>.Failure("No se encontró el rol");
                }

                // Actualizar el nombre
                role.Name = dto.Name;

                // Obtener los permisos seleccionados
                List<Guid> permissionIds = [];

                if (!string.IsNullOrEmpty(dto.PermissionsIds))
                {
                    permissionIds =
                        JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionsIds) ?? [];
                }

                // Eliminar permisos anteriores
                List<RolePermission> oldRolePermissions =
                    await _context.RolePermissions
                        .Where(rp => rp.PrivateRecipeRoleId == role.Id)
                        .ToListAsync();

                _context.RolePermissions.RemoveRange(oldRolePermissions);

                // Agregar los nuevos permisos
                foreach (Guid permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        PrivateRecipeRoleId = role.Id,
                        PermissionId = permissionId
                    };

                    await _context.RolePermissions.AddAsync(rolePermission);
                }

                await _context.SaveChangesAsync();

                return Response<RoleDTO>.Success(dto, "Rol actualizado con éxito");
            }
            catch (Exception ex)
            {
                return Response<RoleDTO>.Failure(ex);
            }
        }
    }
}
