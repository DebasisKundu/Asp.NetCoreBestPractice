using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class City : BaseEntity
    {
        [ForeignKey("State")]
        public int StateId { get; set; }
        public string Name { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<PGInfo> PGInfos { get; set; }
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}
