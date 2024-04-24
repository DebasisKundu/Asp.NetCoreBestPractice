using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class PGBooking : BaseEntity
    {
        [ForeignKey("User")]
        public long SeekerId { get; set; }

        [ForeignKey("PGInfo")]
        public int PGInfoId { get; set; }

        [ForeignKey("PGRoom")]
        public int PGRoomId { get; set; }

        [ForeignKey("PGPackage")]
        public int PackageId { get; set; }
        public int Price { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTo { get; set; }

        public virtual User User { get; set; }
        public virtual PGInfo PGInfo { get; set; }

        public virtual PGRoom PGRoom { get; set; }

        public virtual PGPackage PGPackage { get; set; }
    }
}
