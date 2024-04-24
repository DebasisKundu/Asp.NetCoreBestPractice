using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.PGInfos
{
    public interface IPgInfoService
    {
        void InsertPGInfo(PGInfo pGInfo);
    }
}
