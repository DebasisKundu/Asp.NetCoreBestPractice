using FindMyPG.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyPG.Core.Entities
{
    public class State : BaseEntity
    {
        public State()
        {
            Cities = new HashSet<City>();
        }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }

        public virtual ICollection<PGInfo> PGInfos { get; set; }
    }
}
