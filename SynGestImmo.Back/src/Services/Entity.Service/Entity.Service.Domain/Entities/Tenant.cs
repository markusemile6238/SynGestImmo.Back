using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.Domain.Entities
{
    public class Tenant
    {
        public int EntityId { get; set; }
        public DateTime MoveIndate { get; set; }
        public DateTime moveOutDate { get; set; }
    }
}
