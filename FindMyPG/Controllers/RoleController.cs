using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    //[Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    public class RoleController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpPost]
        //[Route("")]
        public async Task<IActionResult> CreateRole(RoleModel request)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<Role>(request);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                    return OkResult(result, "Role Created");

                return BadRequestResult(result, "Role not created");
            }

            return BadRequestResult(ModelState);
        }

        private void Abc()
        {

        }
        public class TestModel
        {
            public int NTest { get; set; }
        }
    }
}
