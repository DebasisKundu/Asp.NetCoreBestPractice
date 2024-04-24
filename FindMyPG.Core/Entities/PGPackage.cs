using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class PGPackage : BaseEntity
    {
        public int PGInfoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual PGInfo PGInfo { get; set; }

        public virtual ICollection<PGBooking> PGBookings { get; set; }

    }
}
