using System.ComponentModel.DataAnnotations;

namespace FindMyPG.Models
{
    public class PGInfoModel
    {
        public PGInfoModel()
        {
            PGPackageModels = new List<PGPackageModel>();
            PGRoomModels = new List<PGRoomModel>();
        }

        [Required, MaxLength(20, ErrorMessage ="Length should not exceed 20 characters")]
        public string Name { get; set; }

        public long OwnerId { get; set; }

        public int StateId { get; set; }

        public int CityId { get; set; }

        public int ZipId { get; set; }

        public string Landmark { get; set; }

        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number")]
        public string ContactNumber { get; set; }

        public int PGCategory { get; set; } // M -1, F-2

        public List<PGPackageModel> PGPackageModels { get; set; }
        public List<PGRoomModel> PGRoomModels { get; set; }
    }
}
