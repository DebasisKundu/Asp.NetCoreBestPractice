namespace FindMyPG.Models
{
    public class StateModel : BaseModel
    {
        public StateModel()
        {
            Cities = new List<CityModel>();
        }
        public string Name { get; set; }

        public List<CityModel> Cities { get; set; }
    }

    public class StateModelRequest
    {
        public StateModelRequest()
        {
            Cities = new List<CityModelRequest>();
        }
        public string StateName { get; set; }
        public bool IsActive { get; set; }

        public List<CityModelRequest> Cities { get; set; }
    }

    public class StateModelUpdateRequest
    {
        public string StateName { get; set; }
        public bool IsActive { get; set; }

    }
}
