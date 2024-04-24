using FindMyPG.Core.Data;
using FindMyPG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Service.Cities
{
    public class CityService : ICityService
    {

        private readonly IRepository<City> _cityRepository;
        public CityService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public List<City> GetAllActiveCities()
        {
            return _cityRepository.Table
                .Where(a => a.IsActive).ToList();
        }

        public List<City> GetAllActiveCityByStateId(int stateId)
        {
            return _cityRepository.Table
                .Where(s => s.IsActive && s.StateId == stateId).ToList();
        }

        public List<City> GetAllCities()
        {
            return _cityRepository.Table.ToList();
        }

        public City GetCityById(int id)
        {
            return _cityRepository.GetById(id);
        }

        public void InsertCity(City city)
        {
            _cityRepository.Insert(city);
        }

        public void InsertCity(List<City> cities)
        {
            _cityRepository.Insert(cities);
        }

        public void UpdateCity(City city)
        {
            _cityRepository.Update(city);
        }
    }
}
