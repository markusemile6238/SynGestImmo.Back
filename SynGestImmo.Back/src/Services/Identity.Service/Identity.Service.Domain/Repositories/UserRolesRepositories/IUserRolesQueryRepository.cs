using Identity.Service.Domain.Entities;
using Identity.Service.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Domain.Repositories.UserRolesRepositories
{
    public interface IUserRolesQueryRepository
    {
        Task<IEnumerable<Role>> GetRolesOfUserId(Guid id);
        Task<IEnumerable<User>> GetAllUserIdByRoleId(int roleId);
    }
}
