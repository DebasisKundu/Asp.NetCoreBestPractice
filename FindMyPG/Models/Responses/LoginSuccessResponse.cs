namespace FindMyPG.Models.Responses
{
    public class LoginSuccessResponse
    {
        public string Token { get; set; }
        public long ExpireIn { get; set; }
    }
}
