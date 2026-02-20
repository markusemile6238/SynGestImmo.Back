using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Domain.Repositories.RoleRepositories
{
    public interface IRolesQueryRepository
    {

        //queries
        Task<IEnumerable<Role>> GetAllRoleAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role?> GetRoleByIdAsync(int id,IDbConnection conn, IDbTransaction tx);
        Task<bool> IsRoleExistAsync(int id);

    }
}
