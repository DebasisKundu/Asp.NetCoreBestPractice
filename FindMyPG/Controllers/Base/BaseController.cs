using FindMyPG.Extensions;
using FindMyPG.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FindMyPG.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected IActionResult OkResult(object result, string message = null)
        {

            if (string.IsNullOrEmpty(message))
                message = "Success";

            return Ok(new PGResponse(message, "V1", result));
        }

        protected IActionResult BadRequestResult(ModelStateDictionary modelState)
        {
            var allErrors = modelState.AllErrors();

            return BadRequestResult(allErrors, null);
        }

        protected IActionResult BadRequestResult(object result, string message)
        {
            if (string.IsNullOrEmpty(message))
                message = "Bad request";

            var response = new PGErrorResponse(message, StatusCodes.Status400BadRequest, "V1", result);

            return BadRequest(response);
        }
    }
}
