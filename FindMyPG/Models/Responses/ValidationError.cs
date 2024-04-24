namespace FindMyPG.Models.Responses
{
    public class ValidationError
    {
        public ValidationError(string name, string reason)
        {
            Name = name;
            Reason = reason;
        }

        public string Name { get; set; }

        public string Reason { get; set; }
    }
}
