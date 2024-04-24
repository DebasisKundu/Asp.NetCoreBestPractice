using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Configs
{
    public class FindMyPGConfig
    {
        public string SqlConnectionsString { get; set; }
        public string JwtSigningKey { get; set; }

        public string JwtValidAudience { get; set; }

        public string JwtValidIssuer { get; set; }

        public double JwtAccessTokenExpireInMinutes { get; set; }
    }
}
