using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.ZipCodes
{
    public interface IZipCodeService
    {
        List<ZipCode> GetAllActiveZipcodeByCityId(int cityId);
    }
}
