using Identity.Service.Application.DTOS.UserDtos;
using MediatR;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetAllUser
{
    public class GetAllUserQuery : IRequest<CqsResult<IEnumerable<UserDto>>>
    {

    }
}
