using FindMyPG.Core.Entities;
using FindMyPG.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Users
{
    public interface IUserService
    {
        Task<User> GetUserByUserName(string userName);
        Task<Role> GetRolesByUser(User user);

        Task<UserRegistrationResult> RegisterUser(UserRegistrationRequest request);
        Task<IdentityResult> InsertUserAsync(User user, string password, string role);

        Task<LoginResultEnum> ValidateUser(string userName, string password);

        Task<bool> SetAccessTokenAsync(User user, string token, 
            string tokenProvider = UserDefaults.PG_TOKEN_PROVIDER);
    }
}
