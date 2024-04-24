using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class PGInfo : BaseEntity
    {
        public string Name { get; set; }

        [ForeignKey("User")]
        public long OwnerId { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        [ForeignKey("ZipCode")]
        public int ZipId { get; set; }

        public string Landmark { get; set; }

        public string ContactNumber { get; set; }

        public int PGCategory { get; set; } // M -1, F-2

        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual ZipCode ZipCode { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PGPackage> PGPackages { get; set; }
        public virtual ICollection<PGRoom> PGRooms { get; set; }
        public virtual ICollection<PGBooking> PGBookings { get; set; }
    }
}
