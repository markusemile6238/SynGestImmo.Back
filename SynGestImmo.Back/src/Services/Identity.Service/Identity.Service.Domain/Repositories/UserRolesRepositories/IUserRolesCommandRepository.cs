using Identity.Service.Domain.Entities;
using Identity.Service.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Domain.Repositories.UserRolesRepositories
{
    public interface IUserRolesCommandRepository
    {
        Task<bool> AssignRoleToUserAsync(Guid userId,int roleId, IDbConnection? connection=null, IDbTransaction?  transaction=null);
        Task<bool> DeleteUserRoleAsync(Guid userId);
        
    }
}
