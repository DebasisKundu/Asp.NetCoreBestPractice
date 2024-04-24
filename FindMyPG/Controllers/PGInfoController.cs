using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.PGInfos;
using FindMyPG.Service.States;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    //[Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    public class PGInfoController : BaseController
    {
        private readonly IPgInfoService _pgInfoService;
        private readonly IMapper _mapper;
        public PGInfoController(IPgInfoService pgInfoService, IMapper mapper)
        {
            _pgInfoService = pgInfoService;
            _mapper = mapper;
        }

        [HttpPost]
        //[Route("")]
        public IActionResult InsertPGInfo(PGInfoModel request)
        {
            if (ModelState.IsValid)
            {
                 _pgInfoService.InsertPGInfo(_mapper.Map<PGInfo>(request));

                return OkResult(request, "Success");
            }

            return BadRequestResult(ModelState);
        }

        [HttpGet]
        //[Route("")]
        public IActionResult GetPGInfo()
        {
            return Ok("Success");
        }
    }
}
