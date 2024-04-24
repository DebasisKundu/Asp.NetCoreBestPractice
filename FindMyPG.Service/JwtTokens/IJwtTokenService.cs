using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.JwtTokens
{
    public interface IJwtTokenService
    {
        TokenResult GenerateAccessToken(TokenRequest request);
    }
}
