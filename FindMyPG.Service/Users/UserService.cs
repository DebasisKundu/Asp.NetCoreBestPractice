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
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Role> GetRolesByUser(User user)
        {
            var roleNames = await _userManager.GetRolesAsync(user);
            return await _roleManager.FindByNameAsync(roleNames.FirstOrDefault());
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userName);
            }
            return user;
        }

        public async Task<IdentityResult> InsertUserAsync(User user, string password, string role)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                result = await _userManager.AddToRoleAsync(user, role);

            return result;
        }

        public async Task<UserRegistrationResult> RegisterUser(UserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException();

            var result = new UserRegistrationResult();

            if (await GetUserByUserName(request.Email) != null)
            {
                result.AddError("Email already exist");
                return result;
            }

            if (await GetUserByUserName(request.PhoneNumber) != null)
            {
                result.AddError("Phone number already exist");
                return result;
            }

            User user = new User()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            try
            {
                var identityResult = await InsertUserAsync(user, request.Password, request.Role);

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        result.AddError(error.Description);
                    }
                }
                else
                    result.User = user;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    result.AddError(ex.InnerException.Message);
                else
                    result.AddError(ex.Message);
            }

            return result;
        }

        public async Task<bool> SetAccessTokenAsync(User user, string token, string tokenProvider = "FindMyPG")
        {
            IdentityResult result = await SetAuthenticationTokenAsync
                (user, tokenProvider, UserDefaults.TokenTypes.AccessToken, token);
            return result.Succeeded;
        }

        public async Task<LoginResultEnum> ValidateUser(string userName, string password)
        {
            var user = await GetUserByUserName(userName);

            if (user == null)
            {
                return LoginResultEnum.UserNotExist;
            }
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return LoginResultEnum.WrongPassword;
            }
            //if (user.LockoutEnabled && user.LockoutEnd.HasValue && user.LockoutEnd.Value.UtcDateTime > DateTime.UtcNow)
            //{
            //    return LoginResultEnum.LockedOut;
            //}

            if (await _userManager.IsLockedOutAsync(user))
            {
                return LoginResultEnum.LockedOut;
            }

            return LoginResultEnum.Successful;
        }

        private async Task<IdentityResult> SetAuthenticationTokenAsync(User user, string tokenProvider, string tokenName, string token)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(tokenProvider))
                throw new ArgumentNullException(nameof(tokenProvider));
            if (string.IsNullOrEmpty(tokenName))
                throw new ArgumentNullException(nameof(tokenName));
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));

            var result = await _userManager
                .SetAuthenticationTokenAsync(user, tokenProvider, tokenName, token);

            if (!result.Succeeded)
            {
                //TODO: Log Serilog
            }

            return result;
        }
    }
}
