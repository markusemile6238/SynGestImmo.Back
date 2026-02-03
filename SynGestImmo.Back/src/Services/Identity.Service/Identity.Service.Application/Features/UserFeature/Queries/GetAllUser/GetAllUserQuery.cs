using Identity.Service.Application.DTOS.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<CqsResult<IEnumerable<UserDto>>>
    {

    }
}
