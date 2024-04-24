using FindMyPG.Service.JwtTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(TokenResult accessToken)
        {
            AccessToken = accessToken;
            Errors = new List<string>();
        }
        public TokenResult AccessToken { get; set; }

        public IList<string> Errors { get; set; }
        public bool Success => AccessToken != null && AccessToken.Success && !Errors.Any();
        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
