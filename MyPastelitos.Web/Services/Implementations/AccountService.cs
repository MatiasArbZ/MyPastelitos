using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Core;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.DTOs.User;
using MyPastelitos.Web.Services.Abstractions;

namespace MyPastelitos.Web.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUsersService _usersService;



        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IUsersService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usersService = usersService;
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

        public Task<Response<SignInResult>> LoginAsync(LoginDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Response<AccountUserDTO>> UpdateAccountUserAsync(AccountUserDTO dto)
        {
            try
            {

                //TODO: validar usuario
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

