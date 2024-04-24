using FindMyPG.Core.Entities;
using FindMyPG.Service.JwtTokens;
using FindMyPG.Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthenticationService(IUserService userService, IJwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }
        public async Task<AuthenticationResult> AuthenticateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var tokenRequest = new TokenRequest()
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                Role = await _userService.GetRolesByUser(user)
            };

            var accessToken = _jwtTokenService.GenerateAccessToken(tokenRequest);

            if (!accessToken.Success && !await _userService.SetAccessTokenAsync(user, accessToken.Token))
            {
                accessToken.Token = null;
            }

            return new AuthenticationResult(accessToken);
        }
    }
}
