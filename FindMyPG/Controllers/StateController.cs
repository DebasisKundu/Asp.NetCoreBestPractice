using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Models;
using FindMyPG.Service.States;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FindMyPG.Controllers
{
    //[Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    public class StateController : BaseController
    {
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;
        public StateController(IStateService stateService, IMapper mapper)
        {
            _stateService = stateService;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("")]
        public IActionResult GetAllStates()
        {
            var states = _stateService.GetAllStates();
            var stateModels = _mapper.Map<List<StateModel>>(states);
            return Ok(stateModels);
        }

        [HttpGet]
        //[Route("Active")]
        public IActionResult GetAllActiveStates()
        {
            var states = _stateService.GetAllActiveStates();
            var stateModels = _mapper.Map<List<StateModel>>(states);
            return Ok(stateModels);
        }

        [HttpGet]
        //[Route("{id}")]
        public IActionResult GetStateById(int id)
        {
            var state = _stateService.GetStateById(id);
            //var stateModel = new StateModel()
            //{
            //     Id=state.Id,
            //     Name=state.Name
            //};
            var cities = state.Cities;
            var stateModel = _mapper.Map<StateModel>(state);

            return Ok(stateModel);
        }

        [HttpPost]
        //[Route("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public IActionResult InsertState(StateModelRequest request)
        {
            if (_stateService.GetStateByName(request.StateName) == null)
            {
                _stateService.InsertState(_mapper.Map<State>(request));
                return Ok("Success");
            }
            return BadRequest("State already exist");
        }

        [HttpPut]
        //[Route("{id}")]
        public IActionResult UpdateState(int id, StateModelUpdateRequest request)
        {
            var state = _stateService.GetStateById(id);
            if (state != null)
            {
                state.Name = request.StateName;
                state.IsActive = request.IsActive;

                _stateService.UpdateState(state);

                return Ok("Success");
            }
            return BadRequest("State not exist");
        }
    }
}
