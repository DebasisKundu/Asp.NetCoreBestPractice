using Microsoft.AspNetCore.Identity;

namespace FindMyPG.Core.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual PGBooking PGBooking { get; set; } // one-to-one
        public virtual ICollection<PGInfo> PGInfos { get; set; }
    }
}
