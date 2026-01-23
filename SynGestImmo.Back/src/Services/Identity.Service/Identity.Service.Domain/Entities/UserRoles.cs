using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Domain.Entities
{
    public class UserRoles
    {
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime AssignedAt { get; set; }
        public Guid AssignedBy { get; set; }

    }
}
