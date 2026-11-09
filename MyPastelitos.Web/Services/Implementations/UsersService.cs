using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Core.Pagination;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.DTOs.User;
using MyPastelitos.Web.Services.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyPastelitos.Web.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        

        public UsersService(UserManager<User> userManager, DataContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            
        }

        public async Task<Response<IdentityResult>> AddUserAsync(User user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            return new Response<IdentityResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };

        }

        public async Task<Response<UserDTO>> CreateAsync(CreateUserDTO dto)
        {
            try
            {
                User user = _mapper.Map<User>(dto);

                user.Id = Guid.NewGuid().ToString();

                Response<IdentityResult> response = await AddUserAsync(user, dto.Document);

                // TODO:  ENVIAR EL TOKEN POR MAIL PARA CONFIRMACION 

                Response<string> token = await GenerateConfirmationTokenAsync(user);

                await ConfirmUserAsync(user, token.Result);

                UserDTO userDto = _mapper.Map<UserDTO>(user);

                return Response<UserDTO>.Success(userDto, "Usuario Creado con exito");
            }
            catch (Exception ex)
            {
                return Response<UserDTO>.Failure(ex);
            }
        }

        public async Task<Response<PaginationResponse<UserDTO>>> GetPaginationAsync(PaginationRequest request)
        {
            try
            {
                IQueryable<User> queryable = _context.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Filter))
                {
                    queryable = queryable.Where(u => u.FirstName.ToLower().Contains(request.Filter.ToLower())
                                                     || u.LastName.ToLower().Contains(request.Filter.ToLower())
                                                     || u.Document.ToLower().Contains(request.Filter.ToLower())
                                                     || u.Email.ToLower().Contains(request.Filter.ToLower())
                                                     || u.PhoneNumber.ToLower().Contains(request.Filter.ToLower()));
                }

                PagedList<User> list = await PagedList<User>.ToPagedListASync(queryable, request);

                PaginationResponse<UserDTO> dto = new PaginationResponse<UserDTO>
                {
                    List = _mapper.Map<PagedList<UserDTO>>(list),
                    CurrentPage = list.CurrentPage,
                    TotalPages = list.TotalPages,
                    RecordsPerPage = list.RecordsPerPage,
                    TotalCount = list.TotalCount,
                    Filter = request.Filter
                };

                return Response<PaginationResponse<UserDTO>>.Success(dto);
            }
            catch (Exception ex)
            {
                return Response<PaginationResponse<UserDTO>>.Failure(ex);
            }
        }
        

        public async Task<Response<User>> GetUserByEmailAsync(string email)
        {
            try
            {
                User? user = await _context.Users.Include(u => u.PrivateRecipeRole)
                                            .FirstOrDefaultAsync(u => u.Email == email);

                if(user is null)
                {
                    return Response<User>.Failure($"no existe usuario con Email: '{email}'");
                }

                return Response<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Response<User>.Failure(ex);
            }
        }

        public async Task<Response<User>> GetUserByIdAsync(string id)
        {
            try
            {
                User? user = await _context.Users.Include(u => u.PrivateRecipeRole)
                                                          .FirstOrDefaultAsync(u => u.Id == id);
                if (user is null)
                {
                    return Response<User>.Failure($"no existe usuario con id: '{id}'");
                }

                return Response<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Response<User>.Failure(ex);
            }
        }
        public async Task<Response<UserDTO>> GetOneAsync(Guid id)
        {
            try
            {
               Response<User> response = await GetUserByIdAsync(id.ToString());

                if (!response.IsSuccess)
                {
                    return Response<UserDTO>.Failure(response.Message, response.Errors);
                }

                
                UserDTO dto = _mapper.Map<UserDTO>(response.Result);

                return Response<UserDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                return Response<UserDTO>.Failure(ex);
            }
        }

        

        public Task<Response<UserDTO>> UpdateAsync(UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UserDTO>> UpdateUserAsync(UserDTO dto)
        {
            try { 

            Response<User> response = await GetUserByIdAsync(dto.Id.ToString());

            if (!response.IsSuccess)
            {
                return Response<UserDTO>.Failure(response.Message, response.Errors);
            }

            User user = response.Result;
            user.PhoneNumber = dto.Phone;
            user.Document = dto.Document;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Photo = dto.Photo;
            user.PrivateRecipeRoleId = dto.PrivateRecipeRoleId == Guid.Empty ? user.PrivateRecipeRoleId : dto.PrivateRecipeRoleId;

            

            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Response<UserDTO>.Success(dto, "Usuario actualizado con exito");

            }catch (Exception ex)
            {
                return Response<UserDTO>.Failure(ex);
            }
        }
        private async Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token)
        {
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

            return new Response<IdentityResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }
        private async Task<Response<string>> GenerateConfirmationTokenAsync(User user)
        {
            string result = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Response<string>.Success(result);

        }

    }
}