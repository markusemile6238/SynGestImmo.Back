using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.Domain.Entities
{
    public class Tenant
    {
        public Guid EntityId { get; set; }
        public DateTime MoveIndate { get; set; }
        public DateTime MoveOutDate { get; set; }
    }
}
