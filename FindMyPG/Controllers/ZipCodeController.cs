using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.Cities;
using FindMyPG.Service.ZipCodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FindMyPG.Controllers
{
    //[Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    public class ZipCodeController : BaseController
    {
        private readonly IZipCodeService _zipCodeService;
        private readonly IMapper _mapper;
        public ZipCodeController(IZipCodeService zipCodeService, IMapper mapper)
        {
            _zipCodeService = zipCodeService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("{cityId}")]
        public IActionResult GetActiveZipcodesByCityId(int cityId)
        {
            var zipCodes = _mapper.Map<List<ZipCodeModel>>
                (_zipCodeService.GetAllActiveZipcodeByCityId(cityId));

            return Ok(zipCodes);
        }
    }
}
