using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Users
{
    public static class UserDefaults
    {
        public static class Claims
        {
            public const string Sub = "sub";
            public const string Iat = "iat";
            public const string Aud = "aud";
            public const string Email = ClaimTypes.Email;
            public const string PhoneNumber = "phone_number";
            public const string Role = ClaimTypes.Role;
            public const string Name = ClaimTypes.Name;
            public const string NameIdentifier = ClaimTypes.NameIdentifier;
        }

        public static class TokenTypes
        {
            public const string AccessToken = "AccessToken";
        }

        public const string PG_TOKEN_PROVIDER = "FindMyPG";
    }
}
