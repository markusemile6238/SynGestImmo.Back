using Entity.Service.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Service.Domain.Entities
{
    public class Owner
    {
        public Guid EntityId { get; set; }
        public OwnerTypeEnum OwnerType { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
