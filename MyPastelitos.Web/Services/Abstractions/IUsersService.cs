using MyPastelitos.Web.Core;
using Microsoft.AspNetCore.Identity;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.DTOs.Account;
using MyPastelitos.Web.DTOs.User;

namespace MyPastelitos.Web.Services.Abstractions
{
    public interface IUsersService
    {

        public Task <Response<IdentityResult>> AddUserAsync(User user, string password);
        public Task <Response<User>> GetUserByEmailAsync(string email);
        public Task <Response<UserDTO>> UpdateUserAsync(UserDTO dto);
        public Task<Response<User>> GetUserByIdAsync(string id);

    }
}
