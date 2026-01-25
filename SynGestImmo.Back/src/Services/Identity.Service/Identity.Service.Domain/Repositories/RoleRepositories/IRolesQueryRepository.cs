using Identity.Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Domain.Repositories.RoleRepositories
{
    public interface IRolesQueryRepository
    {

        //queries
        Task<CqsResult<IEnumerable<Role>>> GetAllRoleAsync();
        Task<CqsResult<Role>?> GetRoleByIdAsync(int id);
        Task<CqsResult<bool>> IsRoleExistAsync(int id);
    }
}
