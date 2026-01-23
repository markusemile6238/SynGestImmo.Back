using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Infrastructure.Data.Repositories
{
    public interface IRolesRepository
    {
        // commands

        Task<CqsResult> CreateRoleAsync(Role role);
        Task<CqsResult> UpdateRoleAsync(int id, Role role);
        Task<CqsResult> DeleteRoleAsync(int id);
        Task<CqsResult> AssignRoleToUserAsync(Guid assignedBy,Guid assignedAt, int role);
        Task<CqsResult> RemoveRoleFromeUserAsync(Guid assignedBy,Guid assignedAt, int role);

        
        //queries
        CqsResult<IEnumerable<Role>> GetAllRoleAsync();
        CqsResult<Role?> GetRoleById(int id);


    }
}
