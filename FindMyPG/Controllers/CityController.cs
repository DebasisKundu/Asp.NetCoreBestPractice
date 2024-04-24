using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.Cities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FindMyPG.Controllers
{
    [Produces("application/json")]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        public CityController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("")]
        public IActionResult GetAllCities()
        {
            var cityModels = _mapper.Map<List<CityModel>>
                (_cityService.GetAllCities());

            return Ok(cityModels);
        }

        [HttpGet]
        //[Route("Active")]
        public IActionResult GetAllActiveCities()
        {
            var cityModels = _mapper.Map<List<CityModel>>
                (_cityService.GetAllActiveCities());

            return Ok(cityModels);
        }

        [HttpGet]
        //[Route("state/{stateId}")]
        public IActionResult GetAllActiveCityByStateId(int stateId)
        {
            var cityModels = _mapper.Map<List<CityModel>>
                (_cityService.GetAllActiveCityByStateId(stateId));

            return Ok(cityModels);
        }

        [HttpGet]
        //[Route("{id}")]
        public IActionResult GetCityById(int id)
        {
            var cityModel = _mapper.Map<CityModel>
                (_cityService.GetCityById(id));

            return Ok(cityModel);
        }

        [HttpPut]
        //[Route("{id}")]
        public IActionResult UpdateCity(int id, CityModelRequest request)
        {
            var city = _cityService.GetCityById(id);
            if (city != null)
            {
                city.Name = request.CityName;
                city.IsActive = request.IsActive;

                _cityService.UpdateCity(city);

                return Ok("Success");
            }

            return BadRequest("City not exist");
        }
    }
}
