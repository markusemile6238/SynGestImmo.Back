using Identity.Service.Application.DTOS.RoleDto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.RoleFeature.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<CqsResult<List<ResponseRoleDto>>>
    {
    }
}
