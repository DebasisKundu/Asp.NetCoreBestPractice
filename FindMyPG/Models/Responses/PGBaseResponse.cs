namespace FindMyPG.Models.Responses
{
    public class PGBaseResponse
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
