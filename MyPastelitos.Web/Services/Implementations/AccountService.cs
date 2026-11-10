using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.DTOs.User;
using MyPastelitos.Web.Services.Abstractions;
using System.Security.Claims;

namespace MyPastelitos.Web.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUsersService _usersService;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _Context;


        public AccountService(UserManager<User> userManager,
                                                SignInManager<User> signInManager,
                                                IUsersService usersService,
                                                IHttpContextAccessor httpContextAccessor,
                                                DataContext context)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
            _Context = context;
        }
        
        public bool AccountUserIsAuthenticated()
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity is not null && user.Identity.IsAuthenticated;
        }

        public async Task<bool> AccountUserIsAuthorizedAsync(string code)
        {
            ClaimsPrincipal? claimUser = _httpContextAccessor.HttpContext?.User;


                    // confirmar identidad (de la sesion)
            if (claimUser is null)
            {
                return false;
            }
            string email = claimUser.Identity!.Name!;

            Response<User> response = await _usersService.GetUserByEmailAsync(email);

            if (!response.IsSuccess || response.Result is null)
            {
                    return false;

               }
            User user = response.Result;

            if (user.PrivateRecipeRole.Name == Env.SUPER_ADMIN_ROLE_NAME)
            {
                return true;
            }

            return await _Context.Permissions.Include(p => p.RolePermissions)
                                              .AnyAsync(p => p.Code == code &&
                                                     p.RolePermissions.Any(rp => rp.PrivateRecipeRoleId == user.PrivateRecipeRoleId));
            
        }

        public async Task<bool> AccountUserIsSuperAdminAsync()
        {
            
            ClaimsPrincipal? claimUser = _httpContextAccessor.HttpContext?.User;


            // confirmar identidad (de la sesion)
            if (claimUser is null)
            {
                return false;
            }
            string email = claimUser.Identity!.Name!;

            Response<User> response = await _usersService.GetUserByEmailAsync(email);

            if (!response.IsSuccess || response.Result is null)
            {
                return false;

            }
            User user = response.Result;

            return user.PrivateRecipeRole.Name == Env.SUPER_ADMIN_ROLE_NAME;
        }

        public async Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token)
        {
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

            return new Response<IdentityResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is AccountService service &&
                   EqualityComparer<IUsersService>.Default.Equals(_usersService, service._usersService);
        }

        public async Task<Response<string>> GenerateConfirmationTokenAsync(User user)
        {
            string result = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Response<string>.Success(result);

        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_usersService);
        }

        public async Task<Response<SignInResult>> LoginAsync(LoginDTO dto)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

            return new Response<SignInResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Response<AccountUserDTO>> UpdateAccountUserAsync(AccountUserDTO dto)
        {
            try
            {


                ClaimsPrincipal? claimUser = _httpContextAccessor.HttpContext?.User;


                // confirmar identidad (de la sesion)
                if (claimUser is null)
                {
                    return Response<AccountUserDTO>.Failure();
                }
                string email = claimUser.Identity!.Name!;

                Response<User> response = await _usersService.GetUserByEmailAsync(email);

                if (!response.IsSuccess || response.Result is null)
                {
                    return Response<AccountUserDTO>.Failure();

                }
                User accountUser = response.Result;

                if (accountUser.Id != dto.Id.ToString()) 

                {
                    return Response<AccountUserDTO>.Failure();
                }
                
                UserDTO userDto = new UserDTO
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Phone = dto.Phone,
                    Photo = dto.Photo,
                    Document = dto.Document,
                };

                Response<UserDTO> userDtoResponse = await _usersService.UpdateUserAsync(userDto);

                if (userDtoResponse.IsSuccess)
                {
                    return Response<AccountUserDTO>.Success(dto);
                }

                return Response<AccountUserDTO>.Failure(userDtoResponse.Message, userDtoResponse.Errors);

            }
            catch (Exception ex)
            {
                return Response<AccountUserDTO>.Failure(ex);
            }
        }
            
        
    }
}

