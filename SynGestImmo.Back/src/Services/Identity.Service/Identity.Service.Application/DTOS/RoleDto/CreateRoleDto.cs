using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Application.DTOS.RoleDto
{
    public class CreateRoleDto
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty ;
        public bool IsSystemRole { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string? Prefixe { get; set; } = "SGI";
    }


}
