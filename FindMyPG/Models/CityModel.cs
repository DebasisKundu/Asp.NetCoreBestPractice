namespace FindMyPG.Models
{
    public class CityModel : BaseModel
    {
        public int StateId { get; set; }
        public string CityName { get; set; }
    }

    public class CityModelRequest
    {
        public string CityName { get; set; }
        public bool IsActive { get; set; }
    }
}
