using Identity.Service.Domaine.Entities;
using MediatR;
using Tools.Result;

namespace Identity.Service.Application.Features.UserFeature.Queries.GetUserByEntityId
{
    public class GetUserByEntityIdQuery : IRequest<CqsResult<User?>>
    {
        public string EntityId { get; set; } = string.Empty;
    }
}
