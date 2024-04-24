namespace FindMyPG.Models.Responses
{
    public class PGResponse : PGBaseResponse
    {
        public object Result { get; set; }

        public PGResponse(string message, string apiVersion, object result = null,
            int statusCode = StatusCodes.Status200OK)
        {
            Message = message;
            Result = result;
            StatusCode = statusCode;
            Version = apiVersion;
        }
    }
}
