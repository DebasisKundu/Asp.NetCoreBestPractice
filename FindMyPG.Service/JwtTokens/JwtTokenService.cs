using FindMyPG.Core;
using FindMyPG.Core.Configs;
using FindMyPG.Service.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.JwtTokens
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly IOptions<FindMyPGConfig> _configuration;
        public JwtTokenService(JwtSecurityTokenHandler jwtSecurityTokenHandler,
            IOptions<FindMyPGConfig> configuration)
        {
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
            _configuration = configuration;
        }

        public TokenResult GenerateAccessToken(TokenRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var claims = new List<Claim>()
            {
                 new Claim(UserDefaults.Claims.Name, request.UserName),
                 new Claim(UserDefaults.Claims.NameIdentifier, request.UserId.ToString(), ClaimValueTypes.Integer64),
                 new Claim(UserDefaults.Claims.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                 new Claim(UserDefaults.Claims.Email, request.Email),
                 new Claim(UserDefaults.Claims.PhoneNumber, request.PhoneNumber),
                 new Claim(UserDefaults.Claims.Aud, UserDefaults.TokenTypes.AccessToken),
                 new Claim(UserDefaults.Claims.Role, request.Role.Name)
            };

            var expiry = DateTime.Now.AddMinutes(_configuration.Value.JwtAccessTokenExpireInMinutes);

            TokenResult response = GenerateToken(claims, expiry);
            return response;
        }

        private TokenResult GenerateToken(IList<Claim> claims, DateTime expiry)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.JwtSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var notBefore = DateTime.Now;

            var result = new TokenResult();
            try
            {
                var securityToken = new JwtSecurityToken(
                    _configuration.Value.JwtValidIssuer,
                    _configuration.Value.JwtValidAudience,
                    claims,
                    notBefore: notBefore,
                    expires: expiry,
                    signingCredentials: creds
                );

                var token = _jwtSecurityTokenHandler.WriteToken(securityToken);

                result.Token = token;
                result.ExpireIn = CommonHelper.ToUnixTimeSeconds(expiry);
            }
            catch
            {
                //TODO:: Write Log using Serilog
            }
            return result;
        }
    }
}
