using FindMyPG.Core.Data;
using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.ZipCodes
{
    public class ZipCodeService : IZipCodeService
    {
        private readonly IRepository<ZipCode> _zipCodeRepository;

        public ZipCodeService(IRepository<ZipCode> zipCodeRepository)
        {
            _zipCodeRepository = zipCodeRepository;
        }

        public List<ZipCode> GetAllActiveZipcodeByCityId(int cityId)
        {
            return _zipCodeRepository.Table
                 .Where(c => c.IsActive && c.CityId == cityId)
                 .ToList();
        }
    }
}
