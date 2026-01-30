using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;


namespace Identity.Service.Domain.Repositories.RoleRepositories
{
    public interface IRolesCommandRepository
    {
        // commands

        Task CreateRoleAsync(Role role);
        Task UpdateRoleAsync(int id, Role role);
        Task DeleteRoleAsync(int id);
        Task AssignRoleToUserAsync(Guid assignedBy,Guid assignedAt, int role);
        Task RemoveRoleFromeUserAsync(Guid assignedBy,Guid assignedAt, int role);

        



    }
}
