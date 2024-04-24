using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.JwtTokens
{
    public class TokenResult
    {
        public string Token { get; set; }
        public long ExpireIn { get; set; }
        public bool Success => !string.IsNullOrEmpty(Token);
    }
}
