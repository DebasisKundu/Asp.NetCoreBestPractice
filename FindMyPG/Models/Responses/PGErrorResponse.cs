namespace FindMyPG.Models.Responses
{
    public class PGErrorResponse : PGBaseResponse
    {
        public object Error { get; set; }
        public PGErrorResponse(string message, int statusCode, string apiVersion, object error)
        {
            Message = message;
            StatusCode = statusCode;
            Version = apiVersion;
            Error = error;
        }
    }
}
