using Identity.Service.Application.DTOS.RoleDto;
using MediatR;
using Tools.Result;

namespace Identity.Service.Application.Features.RoleFeature.Queries
{
    public class GetRoleByIdQuery : IRequest<CqsResult<ResponseRoleDto>>
    {
        public int Id { get; set; }
    }
}
