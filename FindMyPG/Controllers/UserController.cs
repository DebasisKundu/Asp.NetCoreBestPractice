using AutoMapper;
using FindMyPG.Controllers.Base;
using FindMyPG.Core.Entities;
using FindMyPG.Core.Enums;
using FindMyPG.Models;
using FindMyPG.Models.Responses;
using FindMyPG.Service.Authentication;
using FindMyPG.Service.Users;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPG.Controllers
{
    //[Route("api/v1/[controller]")]
    [Produces("application/json")]
    //[ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <summary>
        /// This function is responsible to create user in the system
        /// </summary>
        /// <param name="request"> Register Model</param>
        /// <returns>Registration Result</returns>
        [HttpPost]
        //[Route("register")]
        public async Task<IActionResult> PostRegister(RegisterModel request)
        {
            if (ModelState.IsValid)
            {
                var userRegistrationRequest = new UserRegistrationRequest(request.FirstName, request.LastName,
                    request.PhoneNumber, request.Email, request.Password, request.Role);

                var result = await _userService.RegisterUser(userRegistrationRequest);

                if (result.Success)
                    return OkResult(result.User);

                return BadRequestResult(result.Errors, "");
            }

            return BadRequestResult(ModelState);
        }

        [HttpPost]
        //[Route("login")]
        public async Task<IActionResult> PostLogin(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new LoginSuccessResponse();
                var loginResult = await _userService.ValidateUser(model.UserName, model.Password);
                switch (loginResult)
                {
                    case LoginResultEnum.Successful:
                        var user = await _userService.GetUserByUserName(model.UserName);
                        var result = await _authenticationService.AuthenticateUser(user);
                        if (result.Success)
                        {
                            response.Token = result.AccessToken.Token;
                            response.ExpireIn = result.AccessToken.ExpireIn;
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error);
                            }
                        }
                        break;
                    case LoginResultEnum.UserNotExist:
                        ModelState.AddModelError(string.Empty, "Wrong credential");
                        break;
                    case LoginResultEnum.WrongPassword:
                        ModelState.AddModelError(string.Empty, "Wrong credential");
                        break;
                    case LoginResultEnum.LockedOut:
                        ModelState.AddModelError(string.Empty, "User locked out");
                        break;
                }

                if (ModelState.IsValid)
                    return OkResult(response);
            }

            return BadRequestResult(ModelState);
        }
    }
}
