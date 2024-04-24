using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class ZipCode : BaseEntity
    {
        public string AreaName { get; set; }

        [MaxLength(6)]
        public int Value { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<PGInfo> PGInfos { get; set; }
    }
}
