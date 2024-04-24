using FindMyPG.Core.Data;
using FindMyPG.Core.Entities;
using FindMyPG.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.PGInfos
{
    public class PGInfoService : IPgInfoService
    {
        private readonly IRepository<PGInfo> _pgInfoRepository;
        public PGInfoService(IRepository<PGInfo> pgInfoRepository)
        {
            _pgInfoRepository = pgInfoRepository;
        }
        public void InsertPGInfo(PGInfo pGInfo)
        {
            _pgInfoRepository.Insert(pGInfo);
        }
    }
}
