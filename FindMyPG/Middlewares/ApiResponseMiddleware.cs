using FindMyPG.Models.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FindMyPG.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            string responseText;
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    context.Response.Body = memoryStream;

                    await _next.Invoke(context);

                    if (context.Response.HasStarted)
                    {
                        return;
                    }

                    var bodyAsText = await FormatResponseAsync(memoryStream);
                    context.Response.Body = originalBodyStream;

                    if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        responseText = bodyAsText;//HandleSuccessResponse(bodyAsText);
                    }
                    else
                    {
                        responseText = HandleNotSuccessResponse(context.Response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    responseText = HandleException(context, ex);
                }

                using (var output = new MemoryStream(Encoding.UTF8.GetBytes(responseText)))
                {
                    context.Response.ContentLength = responseText.Length;
                    await output.CopyToAsync(originalBodyStream);
                }
            }
        }

        private async Task<string> FormatResponseAsync(Stream bodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(bodyStream).ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);

            return bodyText;
        }

        private object HandleSuccessResponse(object body)
        {
            return body;
        }

        private string HandleNotSuccessResponse(int statusCode)
        {
            var pgErrorResponse = statusCode switch
            {
                StatusCodes.Status401Unauthorized =>
                new PGErrorResponse("401 Unauthorized", statusCode, "v1", "This is an authorized endpoint"),
                StatusCodes.Status404NotFound =>
                new PGErrorResponse("URl not found", statusCode, "v1", "Please check URL and Parameter"),
                StatusCodes.Status500InternalServerError =>
                new PGErrorResponse("Error occured", statusCode, "v1", "Some unknown error occured"),
                _ => new PGErrorResponse("Error occured", statusCode, "v1", "Some unknown error occured")
            };

            return ConvertToJsonString(pgErrorResponse);

        }

        private string HandleException(HttpContext context, Exception exception)
        {
            PGErrorResponse apiError;
            if (exception is UnauthorizedAccessException)
            {
                apiError = new PGErrorResponse("UnAuthorized", StatusCodes.Status500InternalServerError,
                    "v1", exception.Message);
            }
            else
            {
                var exceptionMessage = "Some unknown error occured";

                apiError = new PGErrorResponse(exceptionMessage, StatusCodes.Status500InternalServerError, 
                    "v1", exception.Message);
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return ConvertToJsonString(apiError);

        }
        private string ConvertToJsonString(object rawJson)
        {
            return JsonConvert.SerializeObject(rawJson);
        }
    }
}
