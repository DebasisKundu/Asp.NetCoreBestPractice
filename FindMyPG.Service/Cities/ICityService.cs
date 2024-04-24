using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Cities
{
    public interface ICityService
    {
        void InsertCity(City city);
        void InsertCity(List<City> cities);
        void UpdateCity(City city);

        List<City> GetAllCities();
        List<City> GetAllActiveCities();
        List<City> GetAllActiveCityByStateId(int stateId);
        City GetCityById(int id);

    }
}
