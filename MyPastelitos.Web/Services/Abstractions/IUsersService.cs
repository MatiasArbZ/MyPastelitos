using MyPastelitos.Web.Core;
using Microsoft.AspNetCore.Identity;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;

namespace MyPastelitos.Web.Services.Abstractions
{
    public interface IUsersService
    {

        public Task <Response<IdentityResult>> AddUserAsync(User user, string password);
        public Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token);
        public Task<Response<string>> GenerateConfirmationTokenAsync(User user);
        public Task<Response<SignInResult>> LoginAsync(LoginDTO dto);
        public Task LogOutAsync();
    }
}
